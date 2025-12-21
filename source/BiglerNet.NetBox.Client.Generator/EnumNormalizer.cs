using NJsonSchema;
using NSwag;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BiglerNet.NetBox.Client.Generator;

public static class EnumNormalizer
{
    // Keyed by (propertyName, signature) → componentName
    private sealed record EnumKey(string? PropertyName, string Signature);

    public static void NormalizeEnums(OpenApiDocument document)
    {
        var enumRegistry = new Dictionary<EnumKey, string>();
        var components = document.Components.Schemas;

        // 1. Component schemas
        foreach (var schemaPair in document.Components.Schemas.ToList())
        {
            var schema = schemaPair.Value;
            NormalizeSchema(schema, enumRegistry, components, propertyName: null);
        }

        // 2. Paths / operations
        foreach (var path in document.Paths)
        {
            foreach (var opKvp in path.Value)
            {
                var operation = opKvp.Value;

                // Parameters
                foreach (var param in operation.Parameters)
                {
                    if (param.Schema != null)
                    {
                        // Use parameter name as "property name"
                        NormalizeSchema(param.Schema, enumRegistry, components, param.Name);
                    }
                }

                // Request body
                if (operation.RequestBody != null)
                {
                    foreach (var content in operation.RequestBody.Content.Values)
                    {
                        if (content.Schema != null)
                        {
                            // No obvious property name here; pass null
                            NormalizeSchema(content.Schema, enumRegistry, components, propertyName: null);
                        }
                    }
                }

                // Responses
                foreach (var response in operation.Responses.Values)
                {
                    foreach (var content in response.Content.Values)
                    {
                        if (content.Schema != null)
                        {
                            NormalizeSchema(content.Schema, enumRegistry, components, propertyName: null);
                        }
                    }
                }
            }
        }
    }

    private static void NormalizeSchema(
        JsonSchema schema,
        Dictionary<EnumKey, string> enumRegistry,
        IDictionary<string, JsonSchema> components,
        string? propertyName)
    {
        if (schema == null)
            return;

        // Recurse into properties (preserve property name)
        foreach (var propKvp in schema.ActualProperties) // ActualProperties handles allOf/oneOf composition
        {
            var childPropName = propKvp.Key;
            var childSchema = propKvp.Value;
            NormalizeSchema(childSchema, enumRegistry, components, childPropName);
        }

        // Recurse into array items
        if (schema.Item != null)
        {
            NormalizeSchema(schema.Item, enumRegistry, components, propertyName);
        }

        // Recurse into dictionary value types
        if (schema.AdditionalPropertiesSchema != null)
        {
            NormalizeSchema(schema.AdditionalPropertiesSchema, enumRegistry, components, propertyName);
        }

        // Only process inline enums (no $ref)
        if (schema.Reference != null)
            return;

        if (schema.Enumeration == null || schema.Enumeration.Count == 0)
            return;

        // Remove explicit "null" values and null references
        var cleaned = schema.Enumeration
            .Where(v => v != null && !string.Equals(v.ToString(), "null", StringComparison.OrdinalIgnoreCase))
            .ToList();

        // If nothing left after removing "null", just treat as plain type (no enum)
        if (cleaned.Count == 0)
        {
            schema.Enumeration.Clear();
            return;
        }

        schema.Enumeration.Clear();
        foreach (var v in cleaned)
        {
            schema.Enumeration.Add(v);
        }

        // Build a stable signature: "String|A|B"
        var typeName = schema.Type.ToString() ?? "Unknown";
        var valuePart = string.Join("|", cleaned.Select(v => v?.ToString() ?? string.Empty));
        var signature = $"{typeName}|{valuePart}";

        var key = new EnumKey(propertyName, signature);

        if (!enumRegistry.TryGetValue(key, out var componentName))
        {
            componentName = CreateComponentName(propertyName, cleaned);

            // Ensure unique component name
            var baseName = componentName;
            var i = 2;
            while (components.ContainsKey(componentName))
            {
                componentName = baseName + i;
                i++;
            }

            // Create the component schema
            var newSchema = new JsonSchema
            {
                Type = schema.Type,
            };

            foreach (var v in cleaned)
            {
                newSchema.Enumeration.Add(v);
            }

            components[componentName] = newSchema;
            enumRegistry[key] = componentName;
        }

        // Replace inline enum with $ref
        schema.Reference = components[componentName];
        schema.Type = JsonObjectType.Null;
        schema.Enumeration.Clear();
    }

    private static string CreateComponentName(string? propertyName, IEnumerable<object> values)
    {
        if (!string.IsNullOrWhiteSpace(propertyName))
        {
            // Use property name like "cable_end" → "CableEndEnum"
            var pascal = ToPascalCase(propertyName);
            return pascal;// + "Enum";
        }

        // Fallback: derive from values, but be defensive about nulls
        var joined = string.Join(
            "",
            values.Select(v => v?.ToString() ?? string.Empty)
        );

        if (string.IsNullOrWhiteSpace(joined))
            joined = "Value";

        var name = "Enum_" + joined;
        name = Regex.Replace(name, "[^A-Za-z0-9_]", "");
        if (char.IsDigit(name[0]))
            name = "_" + name;

        return name;
    }

    private static string ToPascalCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "Enum";

        // Handle snake_case, kebab-case, etc.
        var parts = Regex.Split(input, @"[^A-Za-z0-9]+")
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .ToArray();

        if (parts.Length == 0)
            return "Enum";

        return string.Concat(parts.Select(p =>
        {
            if (p.Length == 1)
                return p.ToUpperInvariant();
            return char.ToUpperInvariant(p[0]) + p.Substring(1);
        }));
    }

}
