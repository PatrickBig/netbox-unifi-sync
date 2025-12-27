using BiglerNet.NetBox.UnifiSync.DataSeeds;
using DotMake.CommandLine;

namespace BiglerNet.NetBox.UnifiSync.Commands;

[CliCommand(Description = "Seeds data into NetBox", Parent = typeof(RootCommand))]
public class SeedDataCommand : ICliRunAsyncWithContextAndReturn
{
    private readonly TagSeeder _tagSeeder;
    private readonly CustomFieldSeeder _customFieldSeeder;
    private readonly LocalIpamAggregatesSeeder _aggregateSeeder;


    public SeedDataCommand(TagSeeder tagSeeder, CustomFieldSeeder customFieldSeeder, LocalIpamAggregatesSeeder aggregateSeeder)
    {
        _tagSeeder = tagSeeder;
        _customFieldSeeder = customFieldSeeder;
        _aggregateSeeder = aggregateSeeder;
    }
    public async Task<int> RunAsync(CliContext cliContext)
    {
        await _tagSeeder.SeedDataAsync(cliContext.CancellationToken);
        await _customFieldSeeder.SeedDataAsync(cliContext.CancellationToken);
        await _aggregateSeeder.SeedDataAsync(cliContext.CancellationToken);

        return 0;
    }
}
