using BiglerNet.NetBox.UnifiSync.Models.Unifi;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace BiglerNet.NetBox.UnifiSync.Services;
public class UnifiIntegrationClient : ApiClientBase, IUnifiIntegrationClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UnifiIntegrationClient> _logger;
    private const string _basePath = "/proxy/network/integration";

    public UnifiIntegrationClient(HttpClient httpClient, ILogger<UnifiIntegrationClient> logger)
        : base(logger, httpClient)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<PagedResponse<SiteListItem>> ListSitesAsync(CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, _basePath + "/v1/sites");

        var response = await ProcessRequestAsync<PagedResponse<SiteListItem>>(request, cancellationToken);

        if (response == null)
        {
            return new PagedResponse<SiteListItem>();
        }

        return response;
    }

    public async Task<PagedResponse<DeviceListItem>> ListDevicesAsync(Guid siteId, int offset = 0, int limit = 25, CancellationToken cancellationToken = default)
    {
        var uri = _basePath + "/v1/sites/" + siteId + "/devices?offset=" + offset + "&limit=" + limit;

        var response = await _httpClient.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            var body = JsonSerializer.Deserialize<PagedResponse<DeviceListItem>>(await response.Content.ReadAsStreamAsync());

            if (body != null)
            {
                return body;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        throw new InvalidOperationException();
    }

    public async Task<DeviceDetails> GetDeviceAsync(Guid siteId, Guid deviceId, CancellationToken cancellationToken)
    {
        var uri = _basePath + "/v1/sites/" + siteId + "/devices/" + deviceId;

        var response = await _httpClient.GetAsync(uri);
        try
        {

            if (response.IsSuccessStatusCode)
            {
                var body = JsonSerializer.Deserialize<DeviceDetails>(await response.Content.ReadAsStreamAsync());

                if (body != null)
                {
                    return body;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                _logger.LogError("Failed to get device {DeviceId} for site {SiteId}. Status code: {StatusCode}", deviceId, siteId, response.StatusCode);
                _logger.LogError("Response body: {ResponseBody}", responseBody);
                throw new InvalidOperationException();
            }
        }
        catch (JsonException ex)
        {
            // We could not deserialize the content, so just output the body.
            var responseBody = await response.Content.ReadAsStringAsync();
            _logger.LogError(ex, "Failed to deserialize device {DeviceId} for site {SiteId}. Response body: {ResponseBody}", deviceId, siteId, responseBody);
            throw;
        }
    }

    public async Task<PagedResponse<ClientListItem>> ListClientsAsync(Guid siteId, int offset = 0, int limit = 25, CancellationToken cancellationToken = default)
    {
        var uri = _basePath + "/v1/sites/" + siteId + "/clients?offset=" + offset + "&limit=" + limit;

        var response = await _httpClient.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            var body = JsonSerializer.Deserialize<PagedResponse<ClientListItem>>(await response.Content.ReadAsStreamAsync());

            if (body != null)
            {
                return body;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        throw new InvalidOperationException();
    }

    public async Task<PagedResponse<NetworkListItem>> ListNetworksAsync(Guid siteId, int offset = 0, int limit = 25, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, _basePath + $"/v1/sites/{siteId}/networks?offset={offset}&limit={limit}");

        var response = await ProcessRequestAsync<PagedResponse<NetworkListItem>>(request, cancellationToken);

        if (response == null)
        {
            return new PagedResponse<NetworkListItem>();
        }

        return response;
    }

    public async Task<NetworkDetails?> GetNetworkAsync(Guid siteId, Guid networkId, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, _basePath + $"/v1/sites/{siteId}/networks/{networkId}");

        var response = await ProcessRequestAsync<NetworkDetails>(request, cancellationToken);

        return response;
    }
}
