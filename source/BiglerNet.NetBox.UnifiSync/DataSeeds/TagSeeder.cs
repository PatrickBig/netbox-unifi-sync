using BiglerNet.NetBox.Client;
using BiglerNet.NetBox.Client.Models;
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
            Name = "Unifi Sync",
            Slug = "unifi-sync",
            Description = "Resources that were automatically imported from the Unifi Sync process.",
        };

        var tags = await _extrasClient.TagsGetAsync(
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            [tag.Slug],
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null);

        if (tags.Count == 0)
        {
            await _extrasClient.TagsPostAsync(tag, null, null, null, null, null, null, cancellationToken);
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
            await _extrasClient.TagsPatchAsync(tags.Results.First().Id, patchedTag, null, null, null, null, null, null, cancellationToken);
        }
    }
}
