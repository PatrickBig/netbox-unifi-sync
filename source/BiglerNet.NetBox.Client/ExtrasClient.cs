using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text.Json;

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

    public async Task<PaginatedCustomFieldList> ListCustomFieldsAsync(ExtrasCustomFieldFilter filter, CancellationToken cancellationToken = default)
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
        var result = await PatchAsJsonAsync<PatchedTagRequest, Tag>($"/api/extras/tags/{id}/", request, cancellationToken);
        
        if (result == null)
        {
            throw new NetBoxApiClientException(0, "Deserialization resulted in null object.");
        }
        else
        {
            return result;
        }
    }
    
    public async Task<CustomField> PatchCustomFieldAsync(int id, PatchedWritableCustomFieldRequest request, CancellationToken cancellationToken = default)
    {
        var result = await PatchAsJsonAsync<PatchedWritableCustomFieldRequest, CustomField>($"/api/extras/custom-fields/{id}/", request, cancellationToken);

        if (result == null)
        {
            throw new NetBoxApiClientException(0, "Deserialization resulted in null object.");
        }
        else
        {
            return result;
        }
    }
}
