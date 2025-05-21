using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BiglerNet.NetBox.UnifiSync.Models.Unifi;
public record SiteListItem
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("internalReference")]
    public required string InternalReference { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
