using BiglerNet.NetBox.Client;
using BiglerNet.NetBox.UnifiSync.Models.Configuration;
using BiglerNet.NetBox.UnifiSync.Services;
using DotMake.CommandLine;
using Microsoft.Extensions.Configuration;

namespace BiglerNet.NetBox.UnifiSync.Commands;

[CliCommand(Description = "Synchronizes a Unifi site with a NetBox site.", Parent = typeof(RootCommand))]
public class UnifiSyncCommand : ICliRunAsyncWithContextAndReturn
{
    private IEnumerable<UnifiNetBoxMapping> _unifiNetBoxMapping;
    private IUnifiNetworkClient _unifiNetworkClient;
    private INetBoxClient _netBoxClient;
    private readonly IpRangeSync _ipRangeSync;

    public UnifiSyncCommand(IConfiguration configuration, IUnifiNetworkClient unifiNetworkClient, INetBoxClient netBoxClient, IpRangeSync ipRangeSync)
    {
        _unifiNetBoxMapping = configuration.GetSection(nameof(UnifiNetBoxMapping)).Get<IEnumerable<UnifiNetBoxMapping>>() ?? Array.Empty<UnifiNetBoxMapping>();
        _unifiNetworkClient = unifiNetworkClient;
        _netBoxClient = netBoxClient;
        _ipRangeSync = ipRangeSync;
    }

    public async Task<int> RunAsync(CliContext cliContext)
    {
        //var sites = await _unifiNetworkClient.ListSitesAsync(cliContext.CancellationToken);

        // Iterate over each resource type and map for each site
        await _ipRangeSync.PerformSyncAsync(cliContext.CancellationToken);

        return 1;
    }
}
