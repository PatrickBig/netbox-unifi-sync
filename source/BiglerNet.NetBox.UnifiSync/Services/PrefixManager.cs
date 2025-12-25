using BiglerNet.NetBox.Client;
using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using BiglerNet.NetBox.UnifiSync.Models.UnifiNetwork;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BiglerNet.NetBox.UnifiSync.Services;

public class PrefixManager(IIpamClient IpamClient, IUnifiNetworkClient UnifiNetworkClient) : UnifiSiteResourceManagerBase
{
    public override async Task<int> SyncronizeUnifiResourcesAsync(string unifiSiteId, string unifiSiteName, int netBoxSiteId, CancellationToken cancellationToken = default)
    {
        // Start by getting all the existing prefixes in NetBox for this site
        var filter = new IpamPrefixFilterBuilder()
            .Limit(150)
            .Tag.Eq(["managed-by-unifi"])
            .Build();

        var netBoxPrefixes = await IpamClient.ListPrefixesAsync(filter, cancellationToken);
        var unifiNetworks = await UnifiNetworkClient.GetNetworkConfigAsync(unifiSiteName, cancellationToken);

        // Create all VLANs first so the prefixes can reference them
        var unifiVlans = unifiNetworks.Data
            .Where(n => n.Vlan.HasValue)
            .Select(n => new { VlanId = n.Vlan!.Value, n.Name });

        foreach (var vlan in unifiVlans)
        {
            await CreateVlanAsync(vlan.VlanId, vlan.Name, cancellationToken);
        }


        var prefixesToCreate = unifiNetworks.Data
            .Where(n => n.IsInternalNetwork());
            

        foreach (var network in prefixesToCreate)
        {
            await CreatePrefixAsync(network, cancellationToken);
        }
        

        return 0;
    }

    private async Task CreatePrefixAsync(NetworkConfigurationItem unifiNetwork, CancellationToken cancellationToken = default)
    {
        var subnet = IPNetwork2.Parse(unifiNetwork.IpSubnet!);
        var request = new WritablePrefixRequest
        {
            Prefix = subnet.Value,
            Vlan = unifiNetwork.Vlan,
            Status = Status12.Active,
            Tags = [ new NestedTagRequest { Slug = "managed-by-unifi" }],
        };
        await IpamClient.CreatePrefixAsync(request, cancellationToken);
    }

    private async Task CreateVlanAsync(int vlanId, string name, CancellationToken cancellationToken = default)
    {
        var request = new WritableVLANRequest
        {
            Vid = vlanId,
            Name = name,
            Tags = [ new NestedTagRequest { Slug = "managed-by-unifi" }],
            //Qinq_role = QinqRole.Empty,
            Status = Status6.Active,
        };

        var result = await IpamClient.CreateVlanAsync(request, cancellationToken);
    }
}
