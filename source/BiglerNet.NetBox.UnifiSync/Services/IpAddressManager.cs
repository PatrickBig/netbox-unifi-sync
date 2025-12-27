using BiglerNet.NetBox.Client;
using BiglerNet.NetBox.Client.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.UnifiSync.Services;

public class IpAddressManager(IUnifiIntegrationClient unifiIntegrationClient, IUnifiNetworkClient unifiNetworkClient, IIpamClient IpamClient, ILogger<IpAddressManager> Logger) : UnifiSiteResourceManagerBase
{
    private const int PageSize = 50;
    public async Task SyncAsync(Guid unifiSiteId, CancellationToken cancellationToken)
    {
        




        // Get all the client devices since they have IP addresses
    }

    private async Task SyncUnifiDevicesAsync(Guid unifiSiteId, CancellationToken cancellationToken)
    {
        // Get all the Unifi devices since they have IP addresses
        var hasMore = true;
        var offset = 0;

        while (hasMore && !cancellationToken.IsCancellationRequested)
        {
            var unifiDevices = await unifiIntegrationClient.ListDevicesAsync(unifiSiteId, offset, PageSize, cancellationToken);

            foreach (var device in unifiDevices.Data)
            {
                var request = new WritableIPAddressRequest
                {
                    Address = device.IpAddress,
                    //Dns_name = device.
                };
                
            }

            if (unifiDevices.Count >= PageSize)
            {
                offset += PageSize;
            }
            else
            {
                hasMore = false;
            }

        }
    }

    private async Task SyncUnifiClientsAsync(Guid unifiSiteId, CancellationToken cancellationToken)
    {
        // Get all the Unifi devices since they have IP addresses
        var hasMore = true;
        var offset = 0;

        while (hasMore && !cancellationToken.IsCancellationRequested)
        {
            var unifiDevices = await unifiIntegrationClient.ListClientsAsync(unifiSiteId, offset, PageSize, cancellationToken);

            if (unifiDevices.Count >= PageSize)
            {
                offset += PageSize;
            }
            else
            {
                hasMore = false;
            }

        }
    }

    public override async Task<int> SyncronizeUnifiResourcesAsync(string unifiSiteId, Guid unifiExternalId, string unifiSiteName, int netBoxSiteId, CancellationToken cancellationToken = default)
    {
        await unifiNetworkClient.ListDevicesAsync(unifiSiteName, cancellationToken);
        
        await SyncUnifiDevicesAsync(unifiExternalId, cancellationToken);

        return 0;
    }
}
