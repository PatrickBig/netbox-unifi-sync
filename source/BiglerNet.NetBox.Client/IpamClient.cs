using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client;

public class IpamClient : NetBoxApiClientBase, IIpamClient
{
    public IpamClient(HttpClient httpClient)
        : base(httpClient)
    { }

    public Task<IPRange> CreateIpRangeAsync(WritableIPRangeRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Prefix> CreatePrefixAsync(WritablePrefixRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteIpRangeAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteIpRangesAsync(IEnumerable<IPRangeRequest> requests, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeletePrefixAsync(int id, CancellationToken cancellationToken = default)
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

    public async Task<PaginatedPrefixList> ListPrefixesAsync(IpamPrefixFilter filter, CancellationToken cancellationToken = default)
    {
        var queryString = filter.ToQueryString();
        var path = "/api/ipam/prefixes/" + queryString;

        using var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, path);

        var response = await GetResponseAsync<PaginatedPrefixList>(httpRequest, cancellationToken);

        return response;
    }

    public async Task<IPRange> PatchIpRangeAsync(int id, PatchedWritableIPRangeRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PatchAsJsonAsync<PatchedWritableIPRangeRequest, IPRange>($"/api/ipam/ip-ranges/{id}/", request, cancellationToken);

        if (result == null)
        {
            throw new NetBoxApiClientException(0, "Deserialization resulted in null object.");
        }
        else
        {
            return result;
        }
    }

    public Task<IPRange> PatchIpRangesAsync(IEnumerable<IPRangeRequest> requests, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Prefix> PatchPrefixAsync(int id, PatchedWritablePrefixRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PatchAsJsonAsync<PatchedWritablePrefixRequest, Prefix>($"/api/ipam/prefixes/{id}/", request, cancellationToken);

        if (result == null)
        {
            throw new NetBoxApiClientException(0, "Deserialization resulted in null object.");
        }
        else
        {
            return result;
        }
    }

    public Task<IPRange> UpdateIpRangeAsync(int id, WritableIPRangeRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IPRange> UpdateIpRangesAsync(IEnumerable<IPRangeRequest> requests, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
