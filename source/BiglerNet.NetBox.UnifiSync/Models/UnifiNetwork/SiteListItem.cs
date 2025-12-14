using System.Text.Json.Serialization;

namespace BiglerNet.NetBox.UnifiSync.Models.UnifiNetwork;
public class SiteListItem
{
    [JsonPropertyName("anonymous_id")]
    public required string AnonymousId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; set; }

    [JsonPropertyName("_id")]
    public required string Id { get; set; }

    [JsonPropertyName("attr_no_delete")]
    public bool AttrNoDelete { get; set; }

    [JsonPropertyName("attr_hidden_id")]
    public required string AttrHiddenId { get; set; }

    [JsonPropertyName("desc")]
    public required string Desc { get; set; }

    [JsonPropertyName("role")]
    public required string Role { get; set; }

    [JsonPropertyName("role_hotspot")]
    public bool RoleHotspot { get; set; }

    [JsonPropertyName("device_count")]
    public int DeviceCount { get; set; }
}
