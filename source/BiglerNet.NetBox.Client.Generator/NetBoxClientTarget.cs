using NSwag;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client.Generator;

public class NetBoxClientTarget
{
    public NetBoxClientTarget(string clientName)
    {
        ClientName = clientName;
    }
    public string ClientName { get; init; }

    public Dictionary<string, OpenApiPathItem> Paths { get; set; } = new();
}
