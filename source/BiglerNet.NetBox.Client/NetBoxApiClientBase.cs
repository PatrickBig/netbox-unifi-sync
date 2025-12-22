using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BiglerNet.NetBox.Client;

public class NetBoxApiClientBase
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions _patchJsonSerializerOptions = new JsonSerializerOptions()
    {
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
    };

    public NetBoxApiClientBase(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    protected HttpClient HttpClient { get { return _httpClient; } }

    protected JsonSerializerOptions PatchJsonSerializerOptions { get { return _patchJsonSerializerOptions; } }

    protected async Task<TResult> GetResponseAsync<TResult>(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.SendAsync(request, cancellationToken);

        var contentString = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.IsSuccessStatusCode && contentString != null)
        {
            try
            {
                var result = JsonSerializer.Deserialize<TResult>(contentString);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new NetBoxApiClientException(response.StatusCode, contentString, "Response object was null");
                }
            }
            catch (JsonException ex)
            {

                throw new NetBoxApiClientException(response.StatusCode, contentString, "Response object was null", ex);
            }
        }
        else
        {
            throw new NetBoxApiClientException(response.StatusCode, contentString, "The NetBox API returned an error status code (" + response.StatusCode + ")");
        }
    }
}
