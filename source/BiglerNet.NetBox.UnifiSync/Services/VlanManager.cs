using BiglerNet.NetBox.Client;
using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BiglerNet.NetBox.UnifiSync.Services;

public class VlanManager(IIpamClient IpamClient, ILogger<VlanManager> Logger)
{
    /// <summary>
    /// Syncs Netbox Vlans with Unifi Vlans
    /// </summary>
    /// <param name="unifiVlans"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns a dictionary that maps the Netbox Vlan ID to the VLAN number (VID).</returns>

    public async Task<Dictionary<int, int>> SyncVlansAsync(IEnumerable<UnifiVlanDetails> unifiVlans, CancellationToken cancellationToken = default)
    {
        IEnumerable<VLAN> netBoxVlans = await GetNetBoxVlansAsync(cancellationToken);

        // Map to return once complete with the sync operation.
        Dictionary<int,int> vlanMap = netBoxVlans.ToDictionary(v => v.Vid, v => v.Id);

        var unifiByVlan = unifiVlans.ToLookup(u => u.Vid);
        var netBoxByVlan = netBoxVlans.ToLookup(u => u.Vid);

        Logger.LogInformation("Syncing VLANs. Detected {UnifiVlanCount} VLANs from Unifi and {NetBoxVlanCount} VLANs from NetBox", unifiVlans.Count(), netBoxVlans.Count());

        // VLANs that exist in Unifi but not in NetBox → create
        var vlansToCreate = unifiVlans
            .Where(u => !netBoxByVlan.Contains(u.Vid))
            .ToList();

        foreach (var vlan in vlansToCreate)
        {
            var newVlan = await CreateVlanAsync(vlan, cancellationToken);
            vlanMap.Add(newVlan.Vid, newVlan.Id);
        }

        // VLANs that exist in NetBox but not in Unifi → delete
        var vlansToDelete = netBoxVlans
            .Where(n => !unifiByVlan.Contains(n.Vid))
            .Select(n => n.Id);

        foreach (var vlanId in vlansToDelete)
        {
            await IpamClient.DeleteVlanAsync(vlanId, cancellationToken);
            if (vlanMap.ContainsValue(vlanId))
            {
                vlanMap.Remove(vlanMap.First(x => x.Value == vlanId).Key);
            }
        }

        // VLANs that exist in both, but the name differs → update
        var vlansToUpdate = unifiVlans
            .Join(netBoxVlans,
                u => u.Vid,
                n => n.Vid,
                (u, n) => new { Unifi = u, NetBox = n })
            .Where(x => x.Unifi.Name != x.NetBox.Name)
            .Select(x => new
            {
                x.NetBox.Id,   // ID for the PATCH
                x.Unifi.Name,
                x.Unifi.Vid,
            });
        
        foreach (var vlan in vlansToUpdate)
        {
            var updatedVlan = await PatchVlanAsync(vlan.Id, vlan.Vid, vlan.Name, cancellationToken);
            vlanMap.Add(updatedVlan.Vid, updatedVlan.Id);
        }

        return vlanMap;
    }

    private async Task<VLAN> CreateVlanAsync(UnifiVlanDetails unifiVlanInfo, CancellationToken cancellationToken)
    {
        var request = new WritableVLANRequest
        {
            Name = unifiVlanInfo.Name,
            Vid = unifiVlanInfo.Vid,
            Tags = [ new NestedTagRequest{ Slug = "managed-by-unifi" }],
        };

        return await IpamClient.CreateVlanAsync(request, cancellationToken);
    }

    private async Task<VLAN> PatchVlanAsync(int netboxVlanId, int vid, string unifiVlanName, CancellationToken cancellationToken)
    {
        var request = new PatchedWritableVLANRequest
        {
            Name = unifiVlanName,
            Vid = vid,
        };

        return await IpamClient.PatchVlanAsync(netboxVlanId, request, cancellationToken);
    }

    private async Task<IEnumerable<VLAN>> GetNetBoxVlansAsync(CancellationToken cancellationToken)
    {
        // Get all the existing VLANs in NetBox
        var filterBuilder = new IpamVlanFilterBuilder()
            .Limit(150)
            .Tag.Eq(["managed-by-unifi"]);

        var netBoxVlans = new List<VLAN>();
        int offset = 0;
        bool hasMore = true;

        while (hasMore)
        {
            var filter = filterBuilder.Offset(offset).Build();
            var netboxVlanResponse = await IpamClient.ListVlansAsync(filter, cancellationToken);
            
            if (netboxVlanResponse == null || netboxVlanResponse.Results == null || !netboxVlanResponse.Results.Any())
            {
                hasMore = false;
                break;
            }

            netBoxVlans.AddRange(netboxVlanResponse.Results);
            
            // Check if we've retrieved all objects
            if (netboxVlanResponse.Results.Count() < 150)
            {
                hasMore = false;
            }
            else
            {
                offset += 150;
            }
        }

        return netBoxVlans;
    }

    public record UnifiVlanDetails
    {
        public required string Name { get; init; }
        public required int Vid { get; init; }
    }
}


