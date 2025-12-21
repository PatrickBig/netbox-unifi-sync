using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client.Extensions;

public static class NetBoxDependencyInjectionExtensions
{
    public static IServiceCollection AddNetBoxClients(this IServiceCollection services, string baseUrl, string netBoxToken)
    {
        services.AddNetBoxClient<IIpamClient, IpamClient>(baseUrl, netBoxToken);

        return services;
    }

    private static void AddNetBoxClient<TClient, TImplementation>(this IServiceCollection services, string baseUrl, string netBoxToken)
        where TClient : class
        where TImplementation : class, TClient
    {
        services.AddHttpClient<TClient, TImplementation>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(baseUrl);
                c.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", netBoxToken);
            });
    }
}
