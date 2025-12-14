using BiglerNet.NetBox.UnifiSync.Services;
using DotMake.CommandLine;
using Microsoft.Extensions.Logging;
using NetBox.Client;

namespace BiglerNet.NetBox.UnifiSync.Commands;

[CliCommand(Description = "Probes Unifi", Parent = typeof(RootCommand))]
public class UnifiProbeCommand
{
    private readonly IUnifiIntegrationClient _unifiClient;
    private readonly ILogger<UnifiProbeCommand> _logger;
    private readonly IUnifiNetworkClient _unifiNetworkClient;
    //private readonly IIpRangesClient _ipRangesClient;
    private readonly IIpamClient _ipamClient;
    private readonly WanSync _wanSync;

    public UnifiProbeCommand(WanSync wanSync, IUnifiIntegrationClient unifiClient, IUnifiNetworkClient unifiNetworkClient, IIpamClient ipamClient, ILogger<UnifiProbeCommand> logger)
    {
        _unifiClient = unifiClient;
        _unifiNetworkClient = unifiNetworkClient;
        _ipamClient = ipamClient;
        _logger = logger;
        _wanSync = wanSync;
    }

    public async Task<int> RunAsync(CliContext cliContext)
    {
        await _wanSync.RunAsync(cliContext.CancellationToken);

        return 0;

    }
}
