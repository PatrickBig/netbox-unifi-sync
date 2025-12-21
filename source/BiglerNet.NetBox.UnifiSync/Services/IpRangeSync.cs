using BiglerNet.NetBox.Client;
using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.UnifiSync.Models.UnifiNetwork;
using Microsoft.Extensions.Logging;
using StrawberryShake;
using System.Net;

namespace BiglerNet.NetBox.UnifiSync.Services;
public class IpRangeSync
{
    private readonly IIpamClient _ipamClient;
    private readonly IUnifiNetworkClient _unifiNetworkClient;
    private readonly ILogger<IpRangeSync> _logger;
    private readonly INetBoxClient _netBoxClient;

    public IpRangeSync(IIpamClient ipamClient, IUnifiNetworkClient unifiNetworkClient, ILogger<IpRangeSync> logger, INetBoxClient netBoxClient)
    {
        _ipamClient = ipamClient;
        _unifiNetworkClient = unifiNetworkClient;
        _logger = logger;
        _netBoxClient = netBoxClient;
    }

    public async Task<int> PerformSyncAsync(CancellationToken cancellationToken)
    {
        var ranges = await GetNetBoxIpRangesAsync(cancellationToken);

        var sites = await _unifiNetworkClient.ListSitesAsync(cancellationToken);
        foreach (var site in sites.Data)
        {
            var networkConfig = await _unifiNetworkClient.GetNetworkConfigAsync(site.Name, cancellationToken);

            foreach (var network in networkConfig.Data)
            {
                await ProcessUnifiNetworkConfigAsync(ranges, network, cancellationToken);
            }
        }

        return 0;
    }

    private async Task ProcessUnifiNetworkConfigAsync(IEnumerable<IListIpAddressRanges_Ip_range_list> netBoxIpAddressRanges, NetworkConfigurationItem networkConfigurationItem, CancellationToken cancellationToken)
    {
        // Only process ip ranges
        if (networkConfigurationItem.IsIpRange())
        {
            // Check if we are going to do an update or create operation.
            var ipNetwork = IPNetwork2.Parse(networkConfigurationItem.IpSubnet);
            var start = ipNetwork.First.ToString();
            var end = ipNetwork.Last.ToString();

            var existingRange = netBoxIpAddressRanges
                .FirstOrDefault(r =>
                    r.Start_address != null && r.End_address != null
                    && r.Start_address.Split('/').First() == start
                    && r.End_address.Split('/').First() == end);

            if (existingRange != null)
            {
                // Update
                await UpdateExistingNetBoxIpRangeAsync(existingRange, ipNetwork, networkConfigurationItem, cancellationToken);
            }
            else
            {
                // Create
                _logger.LogError("Not yet implemented");
            }
        }
        await Task.Delay(100);
    }

    private async Task CreateProviderAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1);
    }

    private async Task UpdateExistingNetBoxIpRangeAsync(IListIpAddressRanges_Ip_range_list netBoxIpRange, IPNetwork2 ipNetwork, NetworkConfigurationItem unifiNetwork, CancellationToken cancellationToken)
    {
        var ipRangeRequest = new PatchedWritableIPRangeRequest
        {
            Start_address = ipNetwork.First.ToString(),
            End_address = ipNetwork.Last.ToString(),
            Status = Status6.Active,
            Description = $"Unifi Network: {unifiNetwork.Name} (VLAN {unifiNetwork.Vlan}) Subnet {unifiNetwork.IpSubnet}",
            Tags = [
                new NestedTagRequest()
                {
                    Name = "Unifi Sync",
                    Slug = "unifi-sync",

                },
            ],
            Custom_fields = new Dictionary<string, object>()
            {
                { "unifi_unique_id", unifiNetwork.Id },
            },
        };

        _logger.LogInformation("Updating existing IP range in NetBox: {IpSubnet}, NetBox range ID: {NetBoxId}", unifiNetwork.IpSubnet, netBoxIpRange.Id);

        await _ipamClient.
            PatchIpRangeAsync(int.Parse(netBoxIpRange.Id), ipRangeRequest, cancellationToken);
    }

    private async Task<IEnumerable<IListIpAddressRanges_Ip_range_list>> GetNetBoxIpRangesAsync(CancellationToken cancellationToken)
    {
        var offset = 0;
        var limit = 100;
        var hasMore = true;
        var allRanges = new List<NetBox.Client.IListIpAddressRanges_Ip_range_list>();

        while (hasMore)
        {
            var ranges = await _netBoxClient.ListIpAddressRanges.ExecuteAsync(offset, limit, "unifi-sync", cancellationToken);

            if (ranges.IsSuccessResult() && ranges.Data != null)
            {
                allRanges.AddRange(ranges.Data.Ip_range_list);

                // Check if we have more results to process
                if (ranges.Data.Ip_range_list.Count < limit)
                {
                    hasMore = false;
                }
                else
                {
                    offset += limit;
                }
            }
            else
            {
                // We have some type of error, throw it and stop processing
                _logger.LogError("Error retrieving IP address ranges from NetBox: {Errors}", string.Join(", ", string.Join(", ", ranges.Errors.Select(e => e.Message))));
                throw new ArgumentException("Error retrieving IP address ranges from NetBox");
            }
        }

        return allRanges;
    }
}
