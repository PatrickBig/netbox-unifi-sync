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
    private readonly PrefixManager _prefixManager;

    public UnifiSyncCommand(IConfiguration configuration, IUnifiNetworkClient unifiNetworkClient, PrefixManager prefixManager)
    {
        _unifiNetBoxMapping = configuration.GetSection(nameof(UnifiNetBoxMapping)).Get<IEnumerable<UnifiNetBoxMapping>>() ?? Array.Empty<UnifiNetBoxMapping>();
        _unifiNetworkClient = unifiNetworkClient;
        _prefixManager = prefixManager;
    }

    public async Task<int> RunAsync(CliContext cliContext)
    {
        var sites = await _unifiNetworkClient.ListSitesAsync(cliContext.CancellationToken);

        foreach (var site in sites.Data)
        {
            await _prefixManager.SyncronizeUnifiResourcesAsync(site.Id, site.Name, 1, cliContext.CancellationToken);
        }

        return 0;

        // Iterate over each resource type and map for each site
        //return await _ipRangeSync.PerformSyncAsync(cliContext.CancellationToken);
    }
}
