using BiglerNet.NetBox.UnifiSync.Models.UnifiNetwork;
using Microsoft.Extensions.Logging;

namespace BiglerNet.NetBox.UnifiSync.Services;
public class UnifiNetworkClient : ApiClientBase, IUnifiNetworkClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UnifiNetworkClient> _logger;

    public UnifiNetworkClient(HttpClient httpClient, ILogger<UnifiNetworkClient> logger)
        : base(logger, httpClient)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<AggregatedDashboardResponse> GetAggregatedDashboardAsync(string siteName, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"/proxy/network/v2/api/site/{siteName}/aggregated-dashboard?historySeconds=86400");
        var response = await ProcessRequestAsync<AggregatedDashboardResponse>(request, cancellationToken);

        return response ?? throw new InvalidOperationException("Failed to get dashoard data.");
    }

    public async Task<NetworkResponseContainer<NetworkConfigurationItem>> GetNetworkConfigAsync(string siteName, CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"/proxy/network/api/s/{siteName}/rest/networkconf");
        var response = await ProcessRequestAsync<NetworkResponseContainer<NetworkConfigurationItem>>(request, cancellationToken);

        return response ?? new();
    }

    public async Task<NetworkResponseContainer<SiteListItem>> ListSitesAsync(CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/proxy/network/api/self/sites");
        var response = await ProcessRequestAsync<NetworkResponseContainer<SiteListItem>>(request, cancellationToken);

        return response ?? new();
    }
}
