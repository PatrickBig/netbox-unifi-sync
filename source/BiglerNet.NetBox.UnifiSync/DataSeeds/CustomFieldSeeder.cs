using BiglerNet.NetBox.Client;
using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using BiglerNet.NetBox.UnifiSync.Extensions;
using BiglerNet.NetBox.UnifiSync.Interfaces;
using Microsoft.Extensions.Logging;

namespace BiglerNet.NetBox.UnifiSync.DataSeeds;
public class CustomFieldSeeder : INetBoxDataSeed
{
    public const string UnifiUniqueIdCustomFieldName = "unifi_unique_id";
    public const string UnifiSiteIdCustomFieldName = "unifi_site_id";
    public const string UnifiCustomFieldGroupName = "Unifi";

    private readonly IExtrasClient _extrasClient;
    private readonly ILogger<CustomFieldSeeder> _logger;

    public CustomFieldSeeder(IExtrasClient extrasClient, ILogger<CustomFieldSeeder> logger)
    {
        _extrasClient = extrasClient;
        _logger = logger;
    }

    public async Task SeedDataAsync(CancellationToken cancellationToken = default)
    {
        var desiredFields = new[]
        {
            UnifiUniqueIdCustomFieldName,
            UnifiSiteIdCustomFieldName,
        };

        var filter = new ExtrasCustomFieldFilterBuilder()
            //.Name.Eq(desiredFields)
            .GroupName.Eq([UnifiCustomFieldGroupName])
            .Limit(100)
            .Build();

        _logger.LogInformation("Checking for existing custom fields [{FieldNames}]", string.Join(", ", desiredFields));

        var existingFields = await _extrasClient.ListCustomFieldsAsync(filter, cancellationToken);

        var fieldsToCreate = desiredFields.Except(existingFields.Results.Select(f => f.Name));
        var fieldsToUpdate = existingFields.Results.Where(f => desiredFields.Contains(f.Name));
        var fieldsToDelete = existingFields.Results.Where(f => !desiredFields.Contains(f.Name));

        if (existingFields.Count == 0)
        {
            _logger.LogInformation("No custom fields found. Creating");

            // Create the unifi device ID field
            var requestBody = new WritableCustomFieldRequest
            {
                Name = UnifiUniqueIdCustomFieldName,
                Label = "Unifi Unique ID",
                Object_types = [
                    "ipam.iprange",
                    "ipam.ipaddress",
                    "ipam.vlan"
                    ],
                Group_name = "Unifi",
                Is_cloneable = false,
                Description = "The unique identifier for the Unifi resource.\n" +
                "This field is created as part of an automated Unifi sync process.\n" +
                "**Do not remove or alter this custom field**",
                Required = false,
                Type = Type4.Text,
                Ui_editable = UiEditable.No,
                Ui_visible = UiVisible.IfSet,
            };

            _ = await _extrasClient.CreateCustomFieldAsync(requestBody, cancellationToken);
        }
        else
        {
            _logger.LogInformation("Found existing field. Performing update to ensure values match.");

            var originalField = existingFields.Results.First();

            var patchedField = new PatchedWritableCustomFieldRequest
            {
                //Choice_set = originalField.Choice_set.,
                Description = originalField.Description,
                Group_name = originalField.Group_name,
                Is_cloneable = originalField.Is_cloneable,
                Comments = originalField.Comments,
                Default = originalField.Default,
                Label = originalField.Label,
                Name = originalField.Name,
                Related_object_filter = originalField.Related_object_filter,
                Object_types = originalField.Object_types,
                Required= originalField.Required,
                Related_object_type = originalField.Related_object_type,
                Unique = originalField.Unique,
                Validation_maximum = originalField.Validation_maximum,
                Validation_minimum = originalField.Validation_minimum,
                Validation_regex = originalField.Validation_regex,
                Weight = originalField.Weight,
                Search_weight = originalField.Search_weight,

                // These enum values need some special handling/mapping
                //Filter_logic = originalField.Filter_logic,
                //Type = originalField.Type,
                //Ui_editable = originalField.Ui_editable,
                //Ui_visible = originalField.Ui_visible,
            };

            // Handle the funny enum values
            
            if (originalField.Filter_logic?.Value != null)
            {
                patchedField.Filter_logic = originalField.Filter_logic.Value.Value.GetMatchingEnumValue<FilterLogic, Value13>();
            }

            if (originalField.Type?.Value != null)
            {
                patchedField.Type = originalField.Type.Value.Value.GetMatchingEnumValue<Type4, Value12>();
            }

            if (originalField.Ui_editable?.Value != null)
            {
                patchedField.Ui_editable = originalField.Ui_editable.Value.Value.GetMatchingEnumValue<UiEditable, Value15>();
            }

            if (originalField.Ui_visible?.Value != null)
            {
                patchedField.Ui_visible = originalField.Ui_visible.Value.Value.GetMatchingEnumValue<UiVisible, Value14>();
            }

            await _extrasClient.PatchCustomFieldAsync(originalField.Id, patchedField, cancellationToken);
        }
    }



    private async Task AddCustomField(CancellationToken cancellationToken)
    {
        var limit = 100;
        int? offset = null;
        bool reachedEnd = false;

        while (!reachedEnd)
        {
            var filter = new ExtrasCustomFieldFilterBuilder()
                .Name.Eq([UnifiUniqueIdCustomFieldName])
                .Offset(offset)
                .Limit(limit)
                .Build();

            var customFields = await _extrasClient.ListCustomFieldsAsync(filter, cancellationToken);
            
            reachedEnd = customFields.Count < limit;

            offset += limit;

            foreach (var field in customFields.Results)
            {
                //field.
            }
        }
    }
}
