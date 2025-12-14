using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace BiglerNet.NetBox.UnifiSync.Services;

public abstract class ApiClientBase
{
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;

    protected ApiClientBase(ILogger logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    protected async Task<TResponse?> ProcessRequestAsync<TResponse>(HttpRequestMessage request, CancellationToken cancellationToken)
        where TResponse : class
    {
        var response = await _httpClient.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            // Try to deserialize the result from JSON to the specified type.
            try
            {
                _logger.LogDebug("Response was successful with HTTP status code {StatusCode}", response.StatusCode);
                var responseData = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken);


                return responseData;
            }
            catch (JsonException ex)
            {
                // There was a problem deserializing.
                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError(ex, "Failed to deserialize response content: {ResponseContent}", responseContent);

                throw new ApiClientException("Failed to deserialize response from API", ex, (int)response.StatusCode, responseContent);
            }
        }
        else
        {
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new ApiClientException("Failed to process API request", (int)response.StatusCode, responseContent);
        }
    }
}
