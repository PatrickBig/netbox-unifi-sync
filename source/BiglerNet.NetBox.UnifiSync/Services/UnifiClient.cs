using BiglerNet.NetBox.UnifiSync.Models.Unifi;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BiglerNet.NetBox.UnifiSync.Services;
public class UnifiClient : IUnifiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UnifiClient> _logger;
    private const string BasePath = "/proxy/network/integration";

    public UnifiClient(HttpClient httpClient, ILogger<UnifiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<PagedResponse<SiteListItem>> GetSitesAsync()
    {
        var response = await _httpClient.GetAsync(BasePath + "/v1/sites");

        if (response.IsSuccessStatusCode)
        {
            var body = JsonSerializer.Deserialize<PagedResponse<SiteListItem>>(await response.Content.ReadAsStreamAsync());

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

    public async Task<PagedResponse<DeviceListItem>> GetDevicesAsync(Guid siteId, int offset = 0, int limit = 25)
    {
        var uri = BasePath + "/v1/sites/" + siteId + "/devices?offset=" + offset + "&limit=" + limit;

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

    public async Task<DeviceDetails> GetDeviceAsync(Guid siteId, Guid deviceId)
    {
        var uri = BasePath + "/v1/sites/" + siteId + "/devices/" + deviceId;

        var response = await _httpClient.GetAsync(uri);

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

        throw new InvalidOperationException();
    }

    public async Task<PagedResponse<ClientListItem>> GetClientsAsync(Guid siteId, int offset = 0, int limit = 25)
    {
        var uri = BasePath + "/v1/sites/" + siteId + "/clients?offset=" + offset + "&limit=" + limit;

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
}
