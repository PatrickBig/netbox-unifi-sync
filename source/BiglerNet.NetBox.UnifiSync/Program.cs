using BiglerNet.NetBox.UnifiSync.Commands;
using BiglerNet.NetBox.UnifiSync.Services;
using DotMake.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Cli.Ext.ConfigureServices(services =>
{
    //services.AddLogging(builder => builder.AddConsole());
    services.AddHttpClient<IUnifiClient, UnifiClient>(c =>
    {
        c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("UNIFI_BASE_URL") ?? throw new ArgumentNullException("Missing base url"));
        c.DefaultRequestHeaders.Add("X-API-KEY", Environment.GetEnvironmentVariable("UNIFI_API_KEY"));
        c.DefaultRequestHeaders.Add("Accept", "application/json");
    });
});

await Cli.RunAsync<RootCommand>();
