namespace BiglerNet.NetBox.UnifiSync.Interfaces;
public interface IResourceSynchronizer
{
    Task SynchronizeResourcesAsync(CancellationToken cancellationToken);
}
