
using BiglerNet.NetBox.Client;
using BiglerNet.NetBox.Client.Models;

namespace BiglerNet.NetBox.UnifiSync.Services;
public class WanSync
{
    private readonly IUnifiNetworkClient _unifiNetworkClient;
    private readonly ITenancyClient _tenancyClient;

    public WanSync(IUnifiIntegrationClient unifiClient, IUnifiNetworkClient unifiNetworkClient, ITenancyClient tenancyClient)
    {
        _unifiNetworkClient = unifiNetworkClient;
        _tenancyClient = tenancyClient;
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        var sites = await _unifiNetworkClient.ListSitesAsync(cancellationToken);

        foreach (var site in sites.Data)
        {
            var dashboardData = await _unifiNetworkClient.GetAggregatedDashboardAsync(site.Name, cancellationToken);

            // Add the WAN interfaces
            var wans = dashboardData?.wan?.wan_details;
            if (wans != null)
            {
                foreach (var wan in wans.Where(w => w.isp != null && w.isp.name != null))
                {
                    if (wan.isp?.name != null)
                    {
                        var request = new TenantRequest
                        {
                            Name = wan.isp.name,
                            Slug = wan.isp.name.ToLower(),
                        };

                        await _tenancyClient.CreateTenantAsync(request, cancellationToken);
                    }
                }
            }
        }
    }
}
