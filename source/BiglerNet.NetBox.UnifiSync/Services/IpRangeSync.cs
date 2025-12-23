using BiglerNet.NetBox.Client;
using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using BiglerNet.NetBox.UnifiSync.Models.UnifiNetwork;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BiglerNet.NetBox.UnifiSync.Services;
public class IpRangeSync : IUnifiSiteSynchronizer
{
    private readonly IIpamClient _ipamClient;
    private readonly IUnifiNetworkClient _unifiNetworkClient;
    private readonly ILogger<IpRangeSync> _logger;

    public IpRangeSync(IIpamClient ipamClient, IUnifiNetworkClient unifiNetworkClient, ILogger<IpRangeSync> logger)
    {
        _ipamClient = ipamClient;
        _unifiNetworkClient = unifiNetworkClient;
        _logger = logger;
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

    private async Task ProcessUnifiNetworkConfigAsync(IEnumerable<IPRange> netBoxIpAddressRanges, NetworkConfigurationItem networkConfigurationItem, CancellationToken cancellationToken)
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
    }

    private async Task CreateProviderAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1);
    }

    private async Task UpdateExistingNetBoxIpRangeAsync(IPRange netBoxIpRange, IPNetwork2 ipNetwork, NetworkConfigurationItem unifiNetwork, CancellationToken cancellationToken)
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
            PatchIpRangeAsync(netBoxIpRange.Id, ipRangeRequest, cancellationToken);
    }

    private async Task<IEnumerable<IPRange>> GetNetBoxIpRangesAsync(CancellationToken cancellationToken)
    {
        var offset = 0;
        var limit = 100;
        var hasMore = true;
        var allRanges = new List<IPRange>();

        var filter = new IpamIpRangeFilterBuilder()
            .Offset(offset)
            .Limit(limit)
            .Build();

        while (hasMore)
        {
            var ranges = await _ipamClient.ListIpRangesAsync(filter, cancellationToken);

            if (ranges != null && ranges.Results != null && ranges.Count > 0)
            {
                allRanges.AddRange(ranges.Results);

                // Check if we have more results to process
                if (ranges.Count < limit)
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
                _logger.LogError("Error retrieving IP address ranges from NetBox");
                throw new ArgumentException("Error retrieving IP address ranges from NetBox");
            }
        }

        return allRanges;
    }

    public Task<int> SynchronizeSiteAsync(Models.Unifi.SiteListItem site, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
