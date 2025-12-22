using BiglerNet.NetBox.Client.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;

namespace BiglerNet.NetBox.Client.Extensions;

public static class NetBoxDependencyInjectionExtensions
{
    public static IServiceCollection AddNetBoxClients(this IServiceCollection services, string baseUrl, string netBoxToken)
    {
        services.AddTransient<HttpClientLoggingHandler>();
        Console.WriteLine("Base uRL: " + baseUrl);
        Console.WriteLine("Token " + netBoxToken);
        services.AddHttpClient("NetBox", c =>
        {
            c.BaseAddress = new Uri(baseUrl);
            c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", netBoxToken);
        });
            //.ConfigureHttpClient(c =>
            //{
            //    c.BaseAddress = new Uri(baseUrl);
            //    c.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", netBoxToken);
            //});

        services.AddNetBoxClient<IIpamClient, IpamClient>();
        services.AddNetBoxClient<IExtrasClient, ExtrasClient>();

        return services;
    }

    private static void AddNetBoxClient<TClient, TImplementation>(this IServiceCollection services)
        where TClient : class
        where TImplementation : class, TClient
    {
        services.AddHttpClient<TClient, TImplementation>("NetBox")
            .AddHttpMessageHandler<HttpClientLoggingHandler>();
    }
}
