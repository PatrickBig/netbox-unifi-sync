using BiglerNet.NetBox.UnifiSync.Services;
using DotMake.CommandLine;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiglerNet.NetBox.UnifiSync.Commands;

[CliCommand(Description = "Probes Unifi", Parent = typeof(RootCommand))]
public class UnifiProbeCommand
{
    private readonly IUnifiClient _unifiClient;
    private readonly ILogger<UnifiProbeCommand> _logger;

    public UnifiProbeCommand(IUnifiClient unifiClient, ILogger<UnifiProbeCommand> logger)
    {
        _unifiClient = unifiClient;
        _logger = logger;
    }

    public async Task<int> RunAsync()
    {
        var sites = await _unifiClient.GetSitesAsync();

        foreach (var site in sites.Data)
        {
            Console.WriteLine("Site: {0}", site.Name);

            var devices = await _unifiClient.GetDevicesAsync(site.Id);

            Console.WriteLine("Devices:");

            foreach (var device in devices.Data)
            {
                Console.WriteLine("\tDevice: {0}", device.Name);

                var deviceDetails = await _unifiClient.GetDeviceAsync(site.Id, device.Id);

                Console.WriteLine("\t\tPorts:");
                
            }

            Console.WriteLine("Clients:");

            var clients = await _unifiClient.GetClientsAsync(site.Id, 0, 100);

            foreach (var client in clients.Data)
            {
                Console.WriteLine("\tClient: {0} ", client.Name);
            }
        }

        
        return 0;
    }
}
