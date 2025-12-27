using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;

namespace BiglerNet.NetBox.Client;

public class CircuitsClient : NetBoxApiClientBase, ICircuitsClient
{
    public CircuitsClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    private const string _circuitsPath = "/api/circuits/circuits/";
    private const string _providersPath = "/api/circuits/providers/";

    #region Circuits
    public async Task<PaginatedCircuitList> ListCircuitsAsync(TagFilter filter, CancellationToken cancellationToken = default)
    {
        var queryString = filter.ToQueryString();
        var path = _circuitsPath + queryString;

        using var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, path);

        var response = await GetResponseAsync<PaginatedCircuitList>(httpRequest, cancellationToken);

        return response;
    }

    public async Task<Circuit> CreateCircuitAsync(WritableCircuitRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PostAsJsonAsync<WritableCircuitRequest, Circuit>(_circuitsPath, request, cancellationToken);
        return result;
    }

    public async Task<Circuit> PatchCircuitAsync(int id, PatchedWritableCircuitRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PatchAsJsonAsync<PatchedWritableCircuitRequest, Circuit>($"{_circuitsPath}{id}/", request, cancellationToken);
        return result;
    }

    public async Task DeleteCircuitAsync(int id, CancellationToken cancellationToken = default)
    {
        var path = $"{_circuitsPath}{id}/";
        var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Delete, path);
        _ = await HttpClient.SendAsync(request, cancellationToken);
    }
    #endregion

    #region Providers
    public async Task<Provider> CreateProviderAsync(ProviderRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PostAsJsonAsync<ProviderRequest, Provider>(_providersPath, request, cancellationToken);
        return result;
    }

    public async Task DeleteProviderAsync(int id, CancellationToken cancellationToken = default)
    {
        var path = $"{_providersPath}{id}/";
        var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Delete, path);
        _ = await HttpClient.SendAsync(request, cancellationToken);
    }

    public async Task<PaginatedProviderList> ListProvidersAsync(TagFilter filter, CancellationToken cancellationToken = default)
    {
        var queryString = filter.ToQueryString();
        var path = _providersPath + queryString;

        using var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, path);

        var response = await GetResponseAsync<PaginatedProviderList>(httpRequest, cancellationToken);

        return response;
    }

    public async Task<Provider> PatchProviderAsync(int id, PatchedProviderRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PatchAsJsonAsync<PatchedProviderRequest, Provider>($"{_providersPath}{id}/", request, cancellationToken);
        return result;
    }
    #endregion
}
