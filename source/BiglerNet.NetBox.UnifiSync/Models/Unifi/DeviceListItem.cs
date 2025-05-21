using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BiglerNet.NetBox.UnifiSync.Models.Unifi;
public class DeviceListItem
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("model")]
    public string Model { get; set; }

    [JsonPropertyName("macAddress")]
    public string MacAddress { get; set; }

    [JsonPropertyName("ipAddress")]
    public string IpAddress { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("features")]
    public string[] Features { get; set; }

    [JsonPropertyName("interfaces")]
    public string[] Interfaces { get; set; }
}
