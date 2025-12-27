using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;

namespace BiglerNet.NetBox.Client;

public interface ICircuitsClient
{
    // Providers
    public Task<PaginatedProviderList> ListProvidersAsync(TagFilter filter, CancellationToken cancellationToken = default);

    public Task<Provider> CreateProviderAsync(ProviderRequest request, CancellationToken cancellationToken = default);

    public Task<Provider> PatchProviderAsync(int id, PatchedProviderRequest request, CancellationToken cancellationToken = default);

    public Task DeleteProviderAsync(int id, CancellationToken cancellationToken = default);

    // Circuits
    public Task<PaginatedCircuitList> ListCircuitsAsync(TagFilter filter, CancellationToken cancellationToken = default);

    public Task<Circuit> CreateCircuitAsync(WritableCircuitRequest request, CancellationToken cancellationToken = default);

    public Task<Circuit> PatchCircuitAsync(int id, PatchedWritableCircuitRequest request, CancellationToken cancellationToken = default);

    public Task DeleteCircuitAsync(int id, CancellationToken cancellationToken = default);
}
