using BiglerNet.NetBox.Client;
using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using BiglerNet.NetBox.UnifiSync.Models.UnifiNetwork;
using Microsoft.Extensions.Logging;

namespace BiglerNet.NetBox.UnifiSync.Services;

public class IpRangeManager(IIpamClient IpamClient, ILogger<IpRangeManager> Logger)
{
    private const int NetBoxPageSize = 150;

    public async Task SyncronizeUnifiIpRangesAsync(IEnumerable<NetworkConfigurationItem> unifiNetworks, CancellationToken cancellationToken)
    {
        var netBoxIpRanges = await GetNetBoxIpRangesAsync(cancellationToken);

        // Only show items with DHCP ranges set
        var netBoxIpRangesWithDhcp = netBoxIpRanges.Where(r => !string.IsNullOrEmpty(r.Start_address) && !string.IsNullOrEmpty(r.End_address));
        var unifiNetworksWithDhcp = unifiNetworks.Where(n => n.DhcpdStart != null && n.DhcpdStop != null);

        Logger.LogInformation("Syncing IP Ranges. Detected {UnifiIpRangeCount} prefixes from Unifi and {NetBoxIpRangeCount} IP ranges from NetBox", netBoxIpRanges.Count(), unifiNetworksWithDhcp.Count());

        // Create lookup for easier matching
        var unifiRanges = unifiNetworksWithDhcp.Select(n => new
        {
            Start = n.DhcpdStart!,
            End = n.DhcpdStop!,
            Network = n
        }).ToDictionary(r => $"{r.Start}-{r.End}", r => r);

        var netBoxRanges = netBoxIpRangesWithDhcp.Select(r => new
        {
            Start = r.Start_address!,
            End = r.End_address!,
            Range = r
        }).ToDictionary(r => $"{r.Start}-{r.End}", r => r);

        // Find ranges to create (in Unifi but not in NetBox)
        var rangesToCreate = unifiNetworksWithDhcp
            .Where(u => !netBoxRanges.ContainsKey($"{u.DhcpdStart}-{u.DhcpdStop}"))
            .ToList();

        foreach (var network in rangesToCreate)
        {
            await CreateIpRangeAsync(network, cancellationToken);
        }

        // Find ranges to delete (in NetBox but not in Unifi)
        var rangesToDelete = netBoxIpRangesWithDhcp
            .Where(n => !unifiRanges.ContainsKey($"{n.Start_address}-{n.End_address}"))
            .Select(n => n.Id);

        foreach (var rangeId in rangesToDelete)
        {
            await IpamClient.DeleteIpRangeAsync(rangeId, cancellationToken);
        }

        // Find ranges to update (exist in both but need updating)
        var rangesToUpdate = unifiNetworksWithDhcp
            .Join(netBoxIpRangesWithDhcp,
                u => $"{u.DhcpdStart}-{u.DhcpdStop}",
                n => $"{n.Start_address}-{n.End_address}",
                (u, n) => new { Unifi = u, NetBox = n })
            .Where(x => x.Unifi.Name != x.NetBox.Description)
            .Select(x => new
            {
                x.NetBox.Id,
                x.Unifi.Name,
                x.Unifi.DhcpdStart,
                x.Unifi.DhcpdStop
            });

        foreach (var range in rangesToUpdate)
        {
            await PatchIpRangeAsync(range.Id, range.Name, range.DhcpdStart!, range.DhcpdStop!, cancellationToken);
        }
    }

    private async Task<IPRange> CreateIpRangeAsync(NetworkConfigurationItem network, CancellationToken cancellationToken)
    {
        var request = new WritableIPRangeRequest
        {
            Start_address = network.DhcpdStart!,
            End_address = network.DhcpdStop!,
            Description = network.Name,
            Tags = [new NestedTagRequest { Slug = "managed-by-unifi" }]
        };

        Logger.LogInformation("Creating IP range {IPRangeStart} - {IPRangeEnd} in NetBox", request.Start_address, request.End_address);

        return await IpamClient.CreateIpRangeAsync(request, cancellationToken);
    }

    private async Task<IPRange> PatchIpRangeAsync(int netboxRangeId, string name, string startAddress, string endAddress, CancellationToken cancellationToken)
    {
        var request = new PatchedWritableIPRangeRequest
        {
            Start_address = startAddress,
            End_address = endAddress,
            Description = name
        };

        return await IpamClient.PatchIpRangeAsync(netboxRangeId, request, cancellationToken);
    }

    private async Task<IEnumerable<IPRange>> GetNetBoxIpRangesAsync(CancellationToken cancellationToken)
    {
        // Start by getting all the existing prefixes in NetBox for this site
        var filterBuilder = new IpamIpRangeFilterBuilder()
            .Limit(NetBoxPageSize)
            .Tag.Eq(["managed-by-unifi"]);

        var netBoxPrefixes = new List<IPRange>();
        int offset = 0;
        bool hasMore = true;

        while (hasMore)
        {
            var filter = filterBuilder.Offset(offset).Build();

            var netBoxPrefixesPage = await IpamClient.ListIpRangesAsync(filter, cancellationToken);

            if (netBoxPrefixesPage?.Results == null || !netBoxPrefixesPage.Results.Any())
            {
                hasMore = false;
                break;
            }

            netBoxPrefixes.AddRange(netBoxPrefixesPage.Results);

            // Check if there are more results to fetch
            if (netBoxPrefixesPage.Results.Count() < NetBoxPageSize)
            {
                hasMore = false;
            }
            else
            {
                offset += NetBoxPageSize;
            }
        }

        return netBoxPrefixes;
    }
}
