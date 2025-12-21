using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BiglerNet.NetBox.Client;

public class NetBoxApiClientBase
{
    private readonly HttpClient _httpClient;

    public NetBoxApiClientBase(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    protected async Task<TResult> GetResponseAsync<TResult>(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return await response.Content.ReadFromJsonAsync<TResult>(cancellationToken);
        }
        else
        {
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new NetBoxApiClientException(response.StatusCode, content);
        }
    }
}
