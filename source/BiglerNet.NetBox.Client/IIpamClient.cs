using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client;

public interface IIpamClient
{

    public Task<PaginatedIPRangeList> ListIpRangesAsync(IpamIpRangeFilter filter, CancellationToken cancellationToken = default);
    public Task<IPRange> CreateIpRangeAsync(WritableIPRangeRequest request, CancellationToken cancellationToken = default);

    public Task<IPRange> UpdateIpRangesAsync(IEnumerable<IPRangeRequest> requests, CancellationToken cancellationToken = default);

    public Task<IPRange> PatchIpRangesAsync(IEnumerable<IPRangeRequest> requests, CancellationToken cancellationToken = default);

    public Task DeleteIpRangesAsync(IEnumerable<IPRangeRequest> requests, CancellationToken cancellationToken = default);

    public Task<IPRange> GetIpRangeByIdAsync(int id, CancellationToken cancellationToken = default);

    public Task<IPRange> UpdateIpRangeAsync(int id, WritableIPRangeRequest request, CancellationToken cancellationToken = default);

    public Task<IPRange> PatchIpRangeAsync(int id, PatchedWritableIPRangeRequest request, CancellationToken cancellationToken = default);

    public Task DeleteIpRangeAsync(int id, CancellationToken cancellationToken = default);




    // IP Prefixes here
    public Task<PaginatedPrefixList> ListPrefixesAsync(IpamPrefixFilter filter, CancellationToken cancellationToken = default);

    public Task<Prefix> CreatePrefixAsync(WritablePrefixRequest request, CancellationToken cancellationToken = default);

    public Task<Prefix> PatchPrefixAsync(int id, PatchedWritablePrefixRequest request, CancellationToken cancellationToken = default);

    public Task DeletePrefixAsync(int id, CancellationToken cancellationToken = default);


    // Vlan
    public Task<PaginatedVLANList> ListVlansAsync(IpamVlanFilter filter, CancellationToken cancellationToken = default);

    public Task<VLAN> CreateVlanAsync(WritableVLANRequest request, CancellationToken cancellationToken = default);

    public Task<VLAN> PatchVlanAsync(int id, PatchedWritableVLANRequest request, CancellationToken cancellationToken = default);

    public Task DeleteVlanAsync(int id, CancellationToken cancellationToken = default);
}
