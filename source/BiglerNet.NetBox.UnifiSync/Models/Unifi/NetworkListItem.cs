using System.Text.Json.Serialization;

namespace BiglerNet.NetBox.UnifiSync.Models.Unifi;

public class NetworkListItem
{
    [JsonPropertyName("management")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public NetworkManagementType Management { get; set; }

    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    [JsonPropertyName("vlanid")]
    public int VlanId { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object?> Metadata { get; set; } = new();
}

