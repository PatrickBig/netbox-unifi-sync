using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text.Json;
using HttpMethod = System.Net.Http.HttpMethod;

namespace BiglerNet.NetBox.Client;

public class ExtrasClient : NetBoxApiClientBase, IExtrasClient
{
    public ExtrasClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public async Task<CustomField> CreateCustomFieldAsync(WritableCustomFieldRequest request, CancellationToken cancellationToken = default)
    {
        using var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "/api/extras/custom-fields/");
        httpRequest.Content = new StringContent(JsonSerializer.Serialize(request));
        httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

        var response = await GetResponseAsync<CustomField>(httpRequest, cancellationToken);

        return response;
    }

    public async Task<Tag> CreateTagAsync(TagRequest request, CancellationToken cancellationToken = default)
    {
        using var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "/api/extras/tags/");
        httpRequest.Content = new StringContent(JsonSerializer.Serialize(request));
        httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

        var response = await GetResponseAsync<Tag>(httpRequest, cancellationToken);

        return response;
    }

    public async Task<PaginatedCustomFieldList> ListCustomFieldsAsync(ExtrasCustomFilter filter, CancellationToken cancellationToken = default)
    {
        var queryString = filter.ToQueryString();
        var path = "/api/extras/custom-fields/" + queryString;

        using var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, path);

        var response = await GetResponseAsync<PaginatedCustomFieldList>(httpRequest, cancellationToken);

        return response;
    }

    public async Task<PaginatedTagList> ListTagsAsync(ExtrasTagFilter filter, CancellationToken cancellationToken = default)
    {
        var queryString = filter.ToQueryString();
        var path = "/api/extras/tags/" + queryString;

        using var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, path);

        var response = await GetResponseAsync<PaginatedTagList>(httpRequest, cancellationToken);

        return response;
    }

    public async Task<Tag> PatchTagAsync(int id, PatchedTagRequest request, CancellationToken cancellationToken = default)
    {
        var content = new StringContent(JsonSerializer.Serialize(request, PatchJsonSerializerOptions), new MediaTypeHeaderValue(MediaTypeNames.Application.Json));
        using var httpRequest = new HttpRequestMessage(HttpMethod.Patch, $"/api/extras/tags/{id}/");
        httpRequest.Content = content;
        using var response = await HttpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.IsSuccessStatusCode)
        {

            var result = JsonSerializer.Deserialize<Tag>(responseContent);

            if (result == null)
            {
                throw new NetBoxApiClientException(response.StatusCode, "Deserialization resulted in null object.");
            }
            
            return result;
        }

        throw new NetBoxApiClientException(response.StatusCode, responseContent);
    }
}
