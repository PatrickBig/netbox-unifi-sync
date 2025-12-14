using System.Text.Json;
using System.Text.Json.Serialization;

namespace BiglerNet.NetBox.UnifiSync.Models;
public class ModelBase
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalProperties { get; set; }
}
