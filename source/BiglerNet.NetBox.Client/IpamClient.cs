using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;

namespace BiglerNet.NetBox.Client;

public class IpamClient : NetBoxApiClientBase, IIpamClient
{
    public IpamClient(HttpClient httpClient)
        : base(httpClient)
    { }

    #region IP Ranges
    public async Task<IPRange> CreateIpRangeAsync(WritableIPRangeRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PostAsJsonAsync<WritableIPRangeRequest, IPRange>("/api/ipam/ip-ranges/", request, cancellationToken);
        return result;
    }

    public async Task DeleteIpRangeAsync(int id, CancellationToken cancellationToken = default)
    {
        var path = $"/api/ipam/ip-ranges/{id}/";
        var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Delete, path);
        _ = await HttpClient.SendAsync(request, cancellationToken);
    }

    public Task DeleteIpRangesAsync(IEnumerable<IPRangeRequest> requests, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IPRange> GetIpRangeByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedIPRangeList> ListIpRangesAsync(IpamIpRangeFilter filter, CancellationToken cancellationToken = default)
    {
        var queryString = filter.ToQueryString();
        var path = "/api/ipam/ip-ranges/" + queryString;

        using var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, path);

        var response = await GetResponseAsync<PaginatedIPRangeList>(httpRequest, cancellationToken);

        return response;
    }

    public async Task<IPRange> PatchIpRangeAsync(int id, PatchedWritableIPRangeRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PatchAsJsonAsync<PatchedWritableIPRangeRequest, IPRange>($"/api/ipam/ip-ranges/{id}/", request, cancellationToken);
        return result;
    }

    public Task<IPRange> PatchIpRangesAsync(IEnumerable<IPRangeRequest> requests, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IPRange> UpdateIpRangeAsync(int id, WritableIPRangeRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PutAsJsonAsync<WritableIPRangeRequest, IPRange>($"/api/ipam/ip-ranges/{id}/", request, cancellationToken);
        return result;
    }

    public Task<IPRange> UpdateIpRangesAsync(IEnumerable<IPRangeRequest> requests, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Prefixes
    public async Task<Prefix> CreatePrefixAsync(WritablePrefixRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PostAsJsonAsync<WritablePrefixRequest, Prefix>("/api/ipam/prefixes/", request, cancellationToken);

        return result;
    }

    public async Task DeletePrefixAsync(int id, CancellationToken cancellationToken = default)
    {
        var path = $"/api/ipam/prefixes/{id}/";
        var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Delete, path);
        _ = await HttpClient.SendAsync(request, cancellationToken);
    }

    public async Task<PaginatedPrefixList> ListPrefixesAsync(TagFilter filter, CancellationToken cancellationToken = default)
    {
        var queryString = filter.ToQueryString();
        var path = "/api/ipam/prefixes/" + queryString;

        using var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, path);

        var response = await GetResponseAsync<PaginatedPrefixList>(httpRequest, cancellationToken);

        return response;
    }

    public async Task<Prefix> PatchPrefixAsync(int id, PatchedWritablePrefixRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PatchAsJsonAsync<PatchedWritablePrefixRequest, Prefix>($"/api/ipam/prefixes/{id}/", request, cancellationToken);

        return result;
    }
    #endregion Prefixes

    #region VLANs
    public async Task<PaginatedVLANList> ListVlansAsync(IpamVlanFilter filter, CancellationToken cancellationToken = default)
    {
        var queryString = filter.ToQueryString();
        var path = "/api/ipam/vlans/" + queryString;

        using var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, path);

        var response = await GetResponseAsync<PaginatedVLANList>(httpRequest, cancellationToken);

        return response;
    }

    public async Task<VLAN> CreateVlanAsync(WritableVLANRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PostAsJsonAsync<WritableVLANRequest, VLAN>("/api/ipam/vlans/", request, cancellationToken);

        return result;
    }

    public async Task<VLAN> PatchVlanAsync(int id, PatchedWritableVLANRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PatchAsJsonAsync<PatchedWritableVLANRequest, VLAN>($"/api/ipam/vlans/{id}/", request, cancellationToken);

        return result;
    }

    public async Task DeleteVlanAsync(int id, CancellationToken cancellationToken = default)
    {
        var path = $"/api/ipam/vlans/{id}/";

        var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Delete, path);

        _ = await HttpClient.SendAsync(request, cancellationToken);
    }

    #endregion

    #region IP Addresses
    public async Task<PaginatedIPAddressList> ListIpAddressesAsync(TagFilter filter, CancellationToken cancellationToken = default)
    {
        var queryString = filter.ToQueryString();
        var path = "/api/ipam/ip-addresses/" + queryString;

        using var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, path);

        var response = await GetResponseAsync<PaginatedIPAddressList>(httpRequest, cancellationToken);

        return response;
    }

    public async Task<IPAddress> CreateIpAddressAsync(WritableIPAddressRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PostAsJsonAsync<WritableIPAddressRequest, IPAddress>("/api/ipam/ip-addresses/", request, cancellationToken);

        return result;
    }

    public async Task<IPAddress> PatchIpAddressAsync(int id, PatchedWritableIPAddressRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PatchAsJsonAsync<PatchedWritableIPAddressRequest, IPAddress>($"/api/ipam/ip-addresses/{id}/", request, cancellationToken);

        return result;
    }

    public async Task DeleteIpAddressAsync(int id, CancellationToken cancellationToken = default)
    {
        var path = $"/api/ipam/ip-addresses/{id}/";

        var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Delete, path);

        _ = await HttpClient.SendAsync(request, cancellationToken);
    }
    #endregion

    #region RIR
    public async Task<PaginatedRIRList> ListRirsAsync(TagFilter filter, CancellationToken cancellationToken = default)
    {
        var queryString = filter.ToQueryString();
        var path = "/api/ipam/rirs/" + queryString;

        using var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, path);

        var response = await GetResponseAsync<PaginatedRIRList>(httpRequest, cancellationToken);

        return response;
    }

    public async Task<RIR> CreateRirAsync(RIRRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PostAsJsonAsync<RIRRequest, RIR>("/api/ipam/rirs/", request, cancellationToken);
        return result;
    }

    public async Task<RIR> PatchRirAsync(int id, PatchedRIRRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PatchAsJsonAsync<PatchedRIRRequest, RIR>($"/api/ipam/rirs/{id}/", request, cancellationToken);
        return result;
    }

    public async Task DeleteRirAsync(int id, CancellationToken cancellationToken = default)
    {
        var path = $"/api/ipam/rirs/{id}/";
        var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Delete, path);
        _ = await HttpClient.SendAsync(request, cancellationToken);
    }
    #endregion

    #region Aggregates
    public async Task<Aggregate> CreateAggregateAsync(WritableAggregateRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PostAsJsonAsync<WritableAggregateRequest, Aggregate>("/api/ipam/aggregates/", request, cancellationToken);
        return result;
    }

    public async Task DeleteAggregateAsync(int id, CancellationToken cancellationToken = default)
    {
        var path = $"/api/ipam/aggregates/{id}/";
        var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Delete, path);
        _ = await HttpClient.SendAsync(request, cancellationToken);
    }

    public async Task<PaginatedAggregateList> ListAggregatesAsync(TagFilter filter, CancellationToken cancellationToken = default)
    {
        var queryString = filter.ToQueryString();
        var path = "/api/ipam/aggregates/" + queryString;

        using var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, path);

        var response = await GetResponseAsync<PaginatedAggregateList>(httpRequest, cancellationToken);

        return response;
    }

    public async Task<Aggregate> PatchAggregateAsync(int id, PatchedWritableAggregateRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PatchAsJsonAsync<PatchedWritableAggregateRequest, Aggregate>($"/api/ipam/aggregates/{id}/", request, cancellationToken);
        return result;
    }

    public async Task<Aggregate> UpdateAggregateAsync(int id, WritableAggregateRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PutAsJsonAsync<WritableAggregateRequest, Aggregate>($"/api/ipam/aggregates/{id}/", request, cancellationToken);
        return result;
    }
    #endregion
}
