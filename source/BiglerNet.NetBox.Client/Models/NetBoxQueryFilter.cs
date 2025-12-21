using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client.Models;

public abstract class NetBoxQueryFilter
{
    public int? Limit { get; internal set; }

    public int? Offset { get; internal set; }

    protected readonly List<QueryParameter> _parameters = new();

    public List<QueryParameter> Parameters => _parameters;

    public virtual string ToQueryString()
    {
        var pairs = new List<string>();

        if (Limit.HasValue)
            pairs.Add($"limit={Limit.Value}");

        if (Offset.HasValue)
            pairs.Add($"offset={Offset.Value}");

        foreach (var p in Parameters)
        {
            foreach (var v in p.Values)
            {
                pairs.Add($"{p.Name}={Uri.EscapeDataString(v)}");
            }
        }

        return pairs.Count == 0
            ? string.Empty
            : "?" + string.Join("&", pairs);
    }
}
