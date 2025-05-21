using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BiglerNet.NetBox.UnifiSync.Models.Unifi;
public class ClientListItem
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("connectedAt")]
    public DateTime ConnectedAt { get; set; }

    [JsonPropertyName("ipAddress")]
    public string IpAddress { get; set; }

    [JsonPropertyName("access")]
    public ClientAccess Access { get; set; }

    public class ClientAccess
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

}
