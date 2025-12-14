using System.Text.Json.Serialization;

namespace BiglerNet.NetBox.UnifiSync.Models.UnifiNetwork;
public class NetworkResponseContainer<TData>
    where TData : class
{
    [JsonPropertyName("meta")]
    public Dictionary<string, object?> Meta { get; set; } = new();

    [JsonPropertyName("data")]
    public IEnumerable<TData> Data { get; set; } = Array.Empty<TData>();
}
