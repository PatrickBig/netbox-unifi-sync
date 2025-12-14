
using Microsoft.Extensions.Logging;

namespace BiglerNet.NetBox.UnifiSync.Services;
public class UnifiNetworkSync : IUnifiNetworkSync
{
    private IUnifiIntegrationClient _unifiClient;
    private ILogger<UnifiNetworkSync> _logger;

    public UnifiNetworkSync(IUnifiIntegrationClient unifiClient, ILogger<UnifiNetworkSync> logger)
    {
        _unifiClient = unifiClient;
        _logger = logger;
    }

    public Task SyncNetworksAsync(Guid siteId, CancellationToken cancellationToken)
    {
        // Get the network list from Unifi for the specified site first
        //var networkList = _unifiClient.GetNe
        throw new NotImplementedException();
    }
}
