
using BiglerNet.NetBox.Client.Generator;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using System.Runtime.CompilerServices;

var document = await OpenApiDocument.FromFileAsync("netbox.openapi.json");

EnumNormalizer.NormalizeEnums(document);

/*
var toRemove = document.Paths
    .Where(p => !prefixes.Any(pre => p.Key.StartsWith(pre, StringComparison.OrdinalIgnoreCase)))
    .Select(p => p.Key)
    .ToList();

toRemove.ForEach(p => document.Paths.Remove(p));
*/
var generatorSettings = new CSharpClientGeneratorSettings
{
    CSharpGeneratorSettings =
    {
        Namespace = "BiglerNet.NetBox.Client",
        JsonLibrary = NJsonSchema.CodeGeneration.CSharp.CSharpJsonLibrary.SystemTextJson,
        GenerateNullableReferenceTypes = true,
        ClassStyle = NJsonSchema.CodeGeneration.CSharp.CSharpClassStyle.Poco,
        GenerateOptionalPropertiesAsNullable = true,
    },
    GenerateClientClasses = true,
    GenerateExceptionClasses = true,
    ExceptionClass = "NetBoxApiException",
    InjectHttpClient = true,
    DisposeHttpClient = true,
    GenerateBaseUrlProperty = true,
    
};



var generator = new CSharpClientGenerator(document, generatorSettings);



var outputPath = "Generated";
Directory.CreateDirectory(outputPath);

// First generate the models only.
generatorSettings.GenerateClientClasses = false;
generatorSettings.CSharpGeneratorSettings.Namespace = "BiglerNet.NetBox.Client.Models";

var modelCode = generator.GenerateFile();
modelCode = EnumPostProcessor.ProcessEnumValues(modelCode);

var modelsFile = Path.Combine(outputPath, "NetBoxModels.cs");
await File.WriteAllTextAsync(modelsFile, modelCode);


// Get the paths for storage later
var httpClients = new Dictionary<string, NetBoxClientTarget>
{
    {"/api/dcim", new("Dcim") },
    {"/api/circuits", new("Circuits") },
    {"/api/extras", new("Extras") },
    {"/api/ipam", new("Ipam") },
    {"/api/tenancy", new("Tenancy") },
    {"/api/wireless", new("Wireless") },
};

foreach (var path in httpClients)
{
    var openApiPaths = document.Paths.Where(p => p.Key.StartsWith(path.Key, StringComparison.OrdinalIgnoreCase)).ToDictionary();
    httpClients[path.Key].Paths = openApiPaths;
}

// Now generate each of the client classes separately
generatorSettings.GenerateClientClasses = true;
generatorSettings.CSharpGeneratorSettings.Namespace = "BiglerNet.NetBox.Client";
generatorSettings.GenerateDtoTypes = false; // Do not generate the models again
generatorSettings.AdditionalNamespaceUsages = ["BiglerNet.NetBox.Client.Models"];



foreach (var client in httpClients)
{
    Console.WriteLine("Writing client for " + client.Key + " prefix");
    document.Paths.Clear();
    
    foreach (var path in client.Value.Paths)
    {
        document.Paths.Add(path.Key, path.Value);
        Console.WriteLine("\t" + path.Key);
    }
    
    var classOutputLocation = Path.Combine(outputPath, $"NetBox{client.Value.ClientName}Client.cs");
    var classCode = generator.GenerateFile();

    await File.WriteAllTextAsync(classOutputLocation, classCode);
}

