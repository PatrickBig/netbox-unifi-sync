using BiglerNet.NetBox.UnifiSync.Models.Unifi;

namespace BiglerNet.NetBox.UnifiSync.Services;
public interface IUnifiIntegrationClient
{
    Task<PagedResponse<SiteListItem>> ListSitesAsync(CancellationToken cancellationToken);

    Task<PagedResponse<DeviceListItem>> ListDevicesAsync(Guid siteId, int offset = 0, int limit = 25, CancellationToken cancellationToken = default);

    Task<DeviceDetails> GetDeviceAsync(Guid siteId, Guid deviceId, CancellationToken cancellationToken = default);

    Task<PagedResponse<ClientListItem>> ListClientsAsync(Guid siteId, int offset = 0, int limit = 25, CancellationToken cancellationToken = default);

    Task<PagedResponse<NetworkListItem>> ListNetworksAsync(Guid siteId, int offset = 0, int limit = 25, CancellationToken cancellationToken = default);

    Task<NetworkDetails?> GetNetworkAsync(Guid siteId, Guid networkId, CancellationToken cancellationToken = default);
}
