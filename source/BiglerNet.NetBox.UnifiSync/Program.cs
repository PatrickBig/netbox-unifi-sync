using BiglerNet.NetBox.Client.Extensions;
using BiglerNet.NetBox.UnifiSync.Commands;
using BiglerNet.NetBox.UnifiSync.DataSeeds;
using BiglerNet.NetBox.UnifiSync.Models.Configuration;
using BiglerNet.NetBox.UnifiSync.Services;
using DotMake.CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;

Cli.Ext.ConfigureServices(services =>
{
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables()
#if DEBUG
        .AddUserSecrets(typeof(Program).Assembly)
#endif
        .Build();

    services.Configure<UnifiOptions>(configuration.GetSection(nameof(UnifiOptions)));
    services.AddSingleton<IConfiguration>(configuration);
    services.AddSingleton<CookieContainer>();
    services.AddHttpClient<UnifiAuthenticationHandler>()
    .ConfigurePrimaryHttpMessageHandler((sp) =>
    {
        return new HttpClientHandler
        {
            CookieContainer = sp.GetRequiredService<CookieContainer>(),
        };
    });

    services.AddLogging(builder => builder
        .AddConsole()
        .AddConfiguration(configuration.GetSection("Logging")));

    services.AddHttpClient<IUnifiNetworkClient, UnifiNetworkClient>(c =>
    {
        c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("UNIFI_BASE_URL") ?? throw new ArgumentNullException("Missing base url"));
    }).AddHttpMessageHandler<UnifiAuthenticationHandler>()
    .ConfigurePrimaryHttpMessageHandler((sp) =>
    {
        return new HttpClientHandler
        {
            CookieContainer = sp.GetRequiredService<CookieContainer>(),
        };
    });
    ;

    services.AddHttpClient<IUnifiIntegrationClient, UnifiIntegrationClient>(c =>
    {
        c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("UNIFI_BASE_URL") ?? throw new ArgumentNullException("Missing base url"));
        c.DefaultRequestHeaders.Add("X-API-KEY", Environment.GetEnvironmentVariable("UNIFI_API_KEY"));
        c.DefaultRequestHeaders.Add("Accept", "application/json");
    });



    // Add all the Netbox clients required for sync process
    var netboxOptions = configuration.GetSection(nameof(NetBoxOptions)).Get<NetBoxOptions>() ?? throw new InvalidOperationException("Missing NetboxOptions section in configuration.");
    var netboxBaseUrl = new Uri(netboxOptions.BaseUrl);

    services.AddNetBoxClient()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://netbox.biglernet.com/graphql/");
        c.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Token", netboxOptions.ApiKey);
    });

    services.AddNetBoxClients(netboxOptions.BaseUrl, netboxOptions.ApiKey);

    services.AddTransient<TagSeeder>();
    services.AddTransient<CustomFieldSeeder>();

    // Add the sync processes
    services.AddTransient<IpRangeSync>();
    services.AddTransient<WanSync>();
});

await Cli.RunAsync<RootCommand>();
