namespace BiglerNet.NetBox.UnifiSync.Services;

public interface IUnifiNetworkSync
{
    Task SyncNetworksAsync(Guid siteId, CancellationToken cancellationToken);
}
