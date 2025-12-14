using BiglerNet.NetBox.UnifiSync.Services;
using DotMake.CommandLine;

namespace BiglerNet.NetBox.UnifiSync.Commands.UnifiProbe;

[CliCommand(Description = "Lists all Unifi sites associated to an endpoint.", Parent = typeof(UnifiProbeCommand))]
public class ListSitesCommand : ICliRunAsyncWithContextAndReturn
{
    private readonly IUnifiNetworkClient _unifiNetworkClient;

    public ListSitesCommand(IUnifiNetworkClient unifiNetworkClient)
    {
        _unifiNetworkClient = unifiNetworkClient;
    }

    public async Task<int> RunAsync(CliContext cliContext)
    {
        var sites = await _unifiNetworkClient.ListSitesAsync(cliContext.CancellationToken);
        Console.WriteLine("Listing all Unifi sites:" + Environment.NewLine);

        foreach (var site in sites.Data)
        {
            Console.WriteLine("Site Name: {0}, ID: {1}, Description: {2}, Device count: {3}", site.Name, site.Id, site.Desc, site.DeviceCount);
        }

        return 0;
    }
}
