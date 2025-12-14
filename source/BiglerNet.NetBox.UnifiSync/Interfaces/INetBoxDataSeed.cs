namespace BiglerNet.NetBox.UnifiSync.Interfaces;
public interface INetBoxDataSeed
{
    Task SeedDataAsync(CancellationToken cancellationToken = default);
}
