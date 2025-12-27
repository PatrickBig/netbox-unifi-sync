using BiglerNet.NetBox.UnifiSync.Models.UnifiNetwork;

namespace BiglerNet.NetBox.UnifiSync.Services;
public interface IUnifiNetworkClient
{
    Task<NetworkResponseContainer<NetworkConfigurationItem>> GetNetworkConfigAsync(string siteName, CancellationToken cancellationToken = default);

    Task<NetworkResponseContainer<SiteListItem>> ListSitesAsync(CancellationToken cancellationToken = default);

    Task<AggregatedDashboardResponse> GetAggregatedDashboardAsync(string siteName, CancellationToken cancellationToken = default);

    Task ListDevicesAsync(string siteName, CancellationToken cancellationToken = default);
}
