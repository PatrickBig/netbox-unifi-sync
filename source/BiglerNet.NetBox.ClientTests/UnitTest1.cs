using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using System.Text.Json;

namespace BiglerNet.NetBox.ClientTests;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;

    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }


    [Fact]
    public void Test1()
    {
        var filter = new IpamIpRangeFilterBuilder()
            .Limit(50)
            .Offset(null)
            .StartAddress.Eq(["10.0.0.1", "10.0.0.2"]);

        var queryString = filter.Build().ToQueryString();

        _output.WriteLine(queryString);
    }

    [Fact]
    public void TestDeser()
    {
        var json = """
            {"count":1,"next":null,"previous":null,"results":[{"id":1,"url":"https://netbox.biglernet.com/api/extras/tags/1/","display_url":"https://netbox.biglernet.com/extras/tags/1/","display":"Unifi Sync","name":"Unifi Sync","slug":"unifi-sync","color":"9e9e9e","description":"Resources that were automatically imported from the Unifi Sync process.","weight":0,"object_types":[],"tagged_items":6,"created":"2025-12-09T02:22:28.681556Z","last_updated":"2025-12-12T02:40:34.891818Z"}]}
            """;
        var result = JsonSerializer.Deserialize<PaginatedTagList>(json);

        Assert.NotNull(result);
    }
}
