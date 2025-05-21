using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BiglerNet.NetBox.UnifiSync.Models.Unifi;
public class PagedResponse<TItem>
{
    [JsonPropertyName("offset")]
    public long Offset { get; init; }

    [JsonPropertyName("limit")]
    public int Limit { get; init; }

    [JsonPropertyName("count")]
    public int Count { get; init; }

    [JsonPropertyName("totalCount")]
    public long TotalCount { get; init; }

    [JsonPropertyName("data")]
    public IEnumerable<TItem> Data { get; init; } = [];
}
