using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client;

public interface IIpamClient
{
    // IP Ranges
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
    public Task<PaginatedPrefixList> ListPrefixesAsync(TagFilter filter, CancellationToken cancellationToken = default);

    public Task<Prefix> CreatePrefixAsync(WritablePrefixRequest request, CancellationToken cancellationToken = default);

    public Task<Prefix> PatchPrefixAsync(int id, PatchedWritablePrefixRequest request, CancellationToken cancellationToken = default);

    public Task DeletePrefixAsync(int id, CancellationToken cancellationToken = default);


    // Vlan
    public Task<PaginatedVLANList> ListVlansAsync(IpamVlanFilter filter, CancellationToken cancellationToken = default);

    public Task<VLAN> CreateVlanAsync(WritableVLANRequest request, CancellationToken cancellationToken = default);

    public Task<VLAN> PatchVlanAsync(int id, PatchedWritableVLANRequest request, CancellationToken cancellationToken = default);

    public Task DeleteVlanAsync(int id, CancellationToken cancellationToken = default);

    // IP Addresses
    public Task<PaginatedIPAddressList> ListIpAddressesAsync(TagFilter filter, CancellationToken cancellationToken = default);

    public Task<IPAddress> CreateIpAddressAsync(WritableIPAddressRequest request, CancellationToken cancellationToken = default);

    public Task<IPAddress> PatchIpAddressAsync(int id, PatchedWritableIPAddressRequest request, CancellationToken cancellationToken = default);

    public Task DeleteIpAddressAsync(int id, CancellationToken cancellationToken = default);

    // RIRs
    public Task<PaginatedRIRList> ListRirsAsync(TagFilter filter, CancellationToken cancellationToken = default);

    public Task<RIR> CreateRirAsync(RIRRequest request, CancellationToken cancellationToken = default);

    public Task<RIR> PatchRirAsync(int id, PatchedRIRRequest request, CancellationToken cancellationToken = default);

    public Task DeleteRirAsync(int id, CancellationToken cancellationToken = default);

    // Aggregates
    public Task<PaginatedAggregateList> ListAggregatesAsync(TagFilter filter, CancellationToken cancellationToken = default);

    public Task<Aggregate> CreateAggregateAsync(WritableAggregateRequest request, CancellationToken cancellationToken = default);

    public Task<Aggregate> PatchAggregateAsync(int id, PatchedWritableAggregateRequest request, CancellationToken cancellationToken = default);

    public Task DeleteAggregateAsync(int id, CancellationToken cancellationToken = default);
}
