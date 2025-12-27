using BiglerNet.NetBox.Client.Models;

namespace BiglerNet.NetBox.UnifiSync.Constants;
public static class Tags
{
    public const string ManagedByUnifiTagSlug = "managed-by-unifi";

    public static readonly ICollection<NestedTagRequest> ManagedByUnifiNestedTagRequest = [new NestedTagRequest { Slug = ManagedByUnifiTagSlug }];
}
