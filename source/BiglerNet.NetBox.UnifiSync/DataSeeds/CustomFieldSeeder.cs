using BiglerNet.NetBox.Client;
using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using BiglerNet.NetBox.UnifiSync.Interfaces;
using Microsoft.Extensions.Logging;

namespace BiglerNet.NetBox.UnifiSync.DataSeeds;
public class CustomFieldSeeder : INetBoxDataSeed
{
    public const string UnifiUniqueIdCustomFieldName = "unifi_unique_id";

    private readonly IExtrasClient _extrasClient;
    private readonly ILogger<CustomFieldSeeder> _logger;

    public CustomFieldSeeder(IExtrasClient extrasClient)
    {
        _extrasClient = extrasClient;
    }

    public async Task SeedDataAsync(CancellationToken cancellationToken = default)
    {
        var desiredFields = new List<string>
        {
            UnifiUniqueIdCustomFieldName,
        };

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
