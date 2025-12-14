using System.Text.Json.Serialization;

namespace BiglerNet.NetBox.UnifiSync.Models.Unifi;
public class NetworkDetails : NetworkListItem
{
    [JsonPropertyName("dhcpGuarding")]
    public DhcpGuardingOptions? DhcpGuarding { get; set; }
}

public class DhcpGuardingOptions
{
    [JsonPropertyName("trustedDhcpServerIpAddresses")]
    public IEnumerable<string> TrustedDhcpServerIpAddresses { get; set; } = Array.Empty<string>();
}