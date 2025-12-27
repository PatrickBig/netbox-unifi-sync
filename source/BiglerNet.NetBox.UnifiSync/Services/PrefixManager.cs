using BiglerNet.NetBox.Client;
using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using BiglerNet.NetBox.UnifiSync.Models.UnifiNetwork;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Intrinsics.Wasm;
using System.Text;
using static BiglerNet.NetBox.UnifiSync.Services.VlanManager;

namespace BiglerNet.NetBox.UnifiSync.Services;

public class PrefixManager(IIpamClient IpamClient, VlanManager VlanManager, IpRangeManager IpRangeManager, IUnifiNetworkClient UnifiNetworkClient, ILogger<PrefixManager> Logger) : UnifiSiteResourceManagerBase
{
    private const int NetBoxPageSize = 150;

    public override async Task<int> SyncronizeUnifiResourcesAsync(string unifiSiteId, Guid unifiExternalId, string unifiSiteName, int netBoxSiteId, CancellationToken cancellationToken = default)
    {
        var netBoxPrefixes = await GetNetBoxPrefixesAsync(cancellationToken);
        var unifiNetworks = await UnifiNetworkClient.GetNetworkConfigAsync(unifiSiteName, cancellationToken);
        // Remove items that are not subnets/prefixes
        unifiNetworks.Data = unifiNetworks.Data.Where(d => d.IpSubnet != null && IPNetwork2.TryParse(d.IpSubnet, false, out _));
        
        // Create all VLANs first so the prefixes can reference them
        var vlanMap = await UpdateNetBoxVlansAsync(unifiNetworks, cancellationToken);

        // Determine which prefixes need to be created vs deleted
        var unifiByPrefix = unifiNetworks.Data.ToLookup(n => n.IpSubnet);
        var netBoxByPrefix = netBoxPrefixes.ToLookup(p => p.Prefix1);

        Logger.LogInformation("Syncing Prefixes. Detected {UnifiPrefixCount} prefixes from Unifi and {NetBoxPrefixCount} prefixes from NetBox", unifiNetworks.Data.Count(), netBoxPrefixes.Count());

        // Prefixes that exist in Unifi, but not in NetBox -> Create
        var prefixesToCreate = unifiNetworks
            .Data
            .Where(n => n.IpSubnet != null && !netBoxByPrefix.Contains(IPNetwork2.Parse(n.IpSubnet).Value))
            .ToList();

        foreach (var prefix in prefixesToCreate)
        {
            await CreatePrefixAsync(vlanMap, prefix, cancellationToken);
        }


        // Prefixes that exist in NetBox, but not in Unifi -> Delete
        var prefixesToDelete = netBoxPrefixes
            .Where(n => !unifiByPrefix.Contains(n.Prefix1));

        // Prefixes that exist in both -> Update
        var prefixesToUpdate = unifiNetworks
            .Data
            .Where(p => p.IpSubnet != null)
            .Join(netBoxPrefixes,
                u => IPNetwork2.Parse(u.IpSubnet!).Value,
                n => n.Prefix1,
                (u, n) => new { Unifi = u, NetBox = n })
            ;

        

        foreach (var prefix in prefixesToUpdate)
        {
            await PatchPrefixAsync(vlanMap, prefix.NetBox.Id, prefix.Unifi, cancellationToken);
        }
        

        // After all prefixes are updated, add/update the IP ranges for each subnet
        await IpRangeManager.SyncronizeUnifiIpRangesAsync(unifiNetworks.Data, cancellationToken);

        return 0;
    }

    private async Task<Dictionary<int, int>> UpdateNetBoxVlansAsync(NetworkResponseContainer<NetworkConfigurationItem> unifiNetworks, CancellationToken cancellationToken)
    {
        
        var unifiVlans = unifiNetworks.Data
            .Where(n => n.Vlan != null && n.Vlan.HasValue)
            .Select(n => new UnifiVlanDetails
            {
                Name = n.Name,
                Vid = n.Vlan!.Value,
            });

        return await VlanManager.SyncVlansAsync(unifiVlans, cancellationToken);
    }

    private async Task CreatePrefixAsync(Dictionary<int, int> vlanMap, NetworkConfigurationItem unifiNetwork, CancellationToken cancellationToken = default)
    {

        var subnet = IPNetwork2.Parse(unifiNetwork.IpSubnet!);

        Logger.LogInformation("Creating prefix {Prefix} in NetBox as {NetBoxPrefix}", unifiNetwork.IpSubnet, subnet.Value);

        int? vlanId = null;

        if (unifiNetwork.Vlan != null && vlanMap.TryGetValue(unifiNetwork.Vlan.Value, out int value))
        {
            vlanId = value;
        }

        var request = new WritablePrefixRequest
        {
            Prefix = subnet.Value,
            Vlan = vlanId,
            Status = Status12.Active,
            Tags = [ new NestedTagRequest { Slug = "managed-by-unifi" }],
        };

        _ = await IpamClient.CreatePrefixAsync(request, cancellationToken);
    }

    private async Task PatchPrefixAsync(Dictionary<int, int> vlanMap, int netBoxPrefixId, NetworkConfigurationItem unifiNetwork, CancellationToken cancellationToken)
    {
        var subnet = IPNetwork2.Parse(unifiNetwork.IpSubnet!);

        Logger.LogInformation("Updating prefix {Prefix} in NetBox as {NetBoxPrefix}", unifiNetwork.IpSubnet, subnet.Value);

        int? vlanId = null;

        if (unifiNetwork.Vlan != null && vlanMap.TryGetValue(unifiNetwork.Vlan.Value, out int value))
        {
            vlanId = value;
        }


        var request = new PatchedWritablePrefixRequest
        {
            Prefix = subnet.Value,
            Vlan = vlanId,
            Status = Status12.Active,
            Tags = [new NestedTagRequest { Slug = "managed-by-unifi" }],
        };

        _ = await IpamClient.PatchPrefixAsync(netBoxPrefixId, request, cancellationToken);
    }

    private async Task<IEnumerable<Prefix>> GetNetBoxPrefixesAsync(CancellationToken cancellationToken)
    {
        // Start by getting all the existing prefixes in NetBox for this site
        var filterBuilder = new TagFilterBuilder()
            .Limit(NetBoxPageSize)
            .Tag.Eq(["managed-by-unifi"]);

        var netBoxPrefixes = new List<Prefix>();
        int offset = 0;
        bool hasMore = true;

        while (hasMore)
        {
            var filter = filterBuilder.Offset(offset).Build();

            var netBoxPrefixesPage = await IpamClient.ListPrefixesAsync(filter, cancellationToken);

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
