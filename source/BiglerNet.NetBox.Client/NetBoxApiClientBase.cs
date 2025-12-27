using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
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

    protected async Task<TResult> PatchAsJsonAsync<TRequest, TResult>(string requestUri, TRequest requestBody, CancellationToken cancellationToken = default)
        where TRequest : class
        where TResult : class
    {
        using var content = new StringContent(JsonSerializer.Serialize(requestBody, PatchJsonSerializerOptions), new MediaTypeHeaderValue(MediaTypeNames.Application.Json));
        using var request = new HttpRequestMessage(HttpMethod.Patch, requestUri);
        request.Content = content;

        var response = await GetResponseAsync<TResult>(request, cancellationToken);

        return response;
    }

    protected async Task<TResult> PostAsJsonAsync<TRequest, TResult>(string requestUri, TRequest requestBody, CancellationToken cancellationToken = default)
        where TRequest : class
        where TResult : class
    {
        using var content = new StringContent(JsonSerializer.Serialize(requestBody, PatchJsonSerializerOptions), new MediaTypeHeaderValue(MediaTypeNames.Application.Json));
        using var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
        request.Content = content;

        var response = await GetResponseAsync<TResult>(request, cancellationToken);

        return response;
    }

    protected async Task<TResult> PutAsJsonAsync<TRequest, TResult>(string requestUri, TRequest requestBody, CancellationToken cancellationToken = default)
        where TRequest : class
        where TResult : class
    {
        using var content = new StringContent(JsonSerializer.Serialize(requestBody, PatchJsonSerializerOptions), new MediaTypeHeaderValue(MediaTypeNames.Application.Json));
        using var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
        request.Content = content;

        var response = await GetResponseAsync<TResult>(request, cancellationToken);

        return response;
    }

    protected async Task<TResult> GetResponseAsync<TResult>(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.SendAsync(request, cancellationToken);

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


