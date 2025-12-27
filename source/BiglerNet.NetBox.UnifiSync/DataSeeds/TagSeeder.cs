using BiglerNet.NetBox.Client;
using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using BiglerNet.NetBox.UnifiSync.Constants;
using BiglerNet.NetBox.UnifiSync.Interfaces;

namespace BiglerNet.NetBox.UnifiSync.DataSeeds;
public class TagSeeder : INetBoxDataSeed
{
    private readonly IExtrasClient _extrasClient;

    public TagSeeder(IExtrasClient extrasClient)
    {
        _extrasClient = extrasClient;
    }

    public async Task SeedDataAsync(CancellationToken cancellationToken = default)
    {
        var tag = new TagRequest
        {
            Name = "Managed by Unifi",
            Slug = Tags.ManagedByUnifiTagSlug,
            Description = "Resources that were automatically imported from the Unifi Sync process.",
            Color = "05254d",
            Weight = 0,
            Object_types = new List<string>(),
        };

        var filter = new ExtrasTagFilterBuilder()
            .TagSlug.Eq([tag.Slug])
            .Build();

        var tags = await _extrasClient.ListTagsAsync(filter, cancellationToken);

        if (tags.Count == 0)
        {
            await _extrasClient.CreateTagAsync(tag, cancellationToken);
        }
        else
        {
            var originalTag = tags.Results.First();
            var patchedTag = new PatchedTagRequest
            {
                Name = tag.Name,
                Slug = tag.Slug,
                Description = tag.Description,

                // Preserve user color/weight
                Color = originalTag.Color,
                Weight = originalTag.Weight,
            };
            
            _ = await _extrasClient.PatchTagAsync(tags.Results.First().Id, patchedTag, cancellationToken);
        }
    }
}
