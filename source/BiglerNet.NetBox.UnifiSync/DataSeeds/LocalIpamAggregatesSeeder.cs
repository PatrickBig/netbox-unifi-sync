using BiglerNet.NetBox.Client;
using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using BiglerNet.NetBox.UnifiSync.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.UnifiSync.DataSeeds;

public class LocalIpamAggregatesSeeder(ILogger<LocalIpamAggregatesSeeder> Logger, IIpamClient IpamClient) : INetBoxDataSeed
{
    private const string RirName = "RFC1918";
    private const string RirSlug = "rfc1918";
    private const string RirDescription = "Internal RIR representing RFC1918 private address space (not a real Internet registry).";

    private const string Aggregate1Prefix = "10.0.0.0/8";
    private const string Aggregate2Prefix = "172.16.0.0/12";
    private const string Aggregate3Prefix = "192.168.0.0/16";

    public async Task SeedDataAsync(CancellationToken cancellationToken = default)
    {
        var privateRirId = await RirHandlingAsync(cancellationToken);
        await AggregateHandlingAsync(privateRirId, cancellationToken);
    }

    private async Task<int> RirHandlingAsync(CancellationToken cancellationToken)
    {
        var filter = new TagFilterBuilder()
            .Tag.Eq(["managed-by-unifi"])
            .Limit(100)
            .Build();
        var rirs = await IpamClient.ListRirsAsync(filter, cancellationToken);

        // We should only have a single RIR
        var privateRir = rirs.Results?.FirstOrDefault(r => r.Slug == "private");
        if (privateRir == null)
        {
            // Create a new one
            var request = new RIRRequest
            {
                Name = RirName,
                Slug = RirSlug,
                Description = RirDescription,
                Tags = [new NestedTagRequest { Slug = "managed-by-unifi" }],
                Is_private = true,
            };

            Logger.LogInformation($"Creating new RIR for private address space for {RirName}");

            var rir = await IpamClient.CreateRirAsync(request, cancellationToken);

            return rir.Id;
        }
        else
        {
            // Do an update
            var request = new PatchedRIRRequest
            {
                Name = RirName,
                Slug = RirSlug,
                Description = RirDescription,
                Is_private = true,
                Tags = [new NestedTagRequest { Slug = "managed-by-unifi" }]
            };

            Logger.LogInformation($"Updating RIR for private address space for {RirName}");

            _ = await IpamClient.PatchRirAsync(privateRir.Id, request, cancellationToken);

            return privateRir.Id;
        }
    }

    private async Task AggregateHandlingAsync(int privateRirId, CancellationToken cancellationToken)
    {
        var filter = new TagFilterBuilder()
            .Tag.Eq(["managed-by-unifi"])
            .Limit(100)
            .Build();

        var aggregates = await IpamClient.ListAggregatesAsync(filter, cancellationToken);

        await ProcessAggregateAsync(privateRirId, Aggregate1Prefix, aggregates.Results, cancellationToken);
        await ProcessAggregateAsync(privateRirId, Aggregate2Prefix, aggregates.Results, cancellationToken);
        await ProcessAggregateAsync(privateRirId, Aggregate3Prefix, aggregates.Results, cancellationToken);
    }

    private async Task ProcessAggregateAsync(int privateRirId, string aggregatePrefix, IEnumerable<Aggregate> existingAggregates, CancellationToken cancellationToken)
    {
        var existingAggregate = existingAggregates.SingleOrDefault(a => a.Prefix == aggregatePrefix);

        if (existingAggregate == null)
        {
            // Create the new aggregate
            var request = new WritableAggregateRequest
            {
                Prefix = aggregatePrefix,
                Date_added = DateTime.UtcNow,
                Rir = privateRirId,
                Description = "RFC1918 private address space reserved for internal use (documentation aggregate)",
                Tags = [new NestedTagRequest { Slug = "managed-by-unifi" }]
            };

            _ = await IpamClient.CreateAggregateAsync(request, cancellationToken);
        }
    }
}
