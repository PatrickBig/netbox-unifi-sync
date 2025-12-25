using BiglerNet.NetBox.UnifiSync.Models.UnifiNetwork;

namespace BiglerNet.NetBox.UnifiSync;
public static class NetworkConfigurationItemExtensions
{
    public static bool IsInternalNetwork(this NetworkConfigurationItem item)
    {
        return !string.IsNullOrEmpty(item.IpSubnet);
    }
}
