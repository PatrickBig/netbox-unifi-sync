using BiglerNet.NetBox.Client.Models;

namespace BiglerNet.NetBox.Client.QueryFilters;

public sealed class ExtrasCustomFilter : NetBoxQueryFilter
{
}

public class ExtrasCustomFilterBuilder
    : NetBoxQueryFilterBuilder<ExtrasCustomFilterBuilder, ExtrasCustomFilter>
{
    public ExtrasCustomFilterBuilder()
    {

        Tag = Field<string[]>("tag", FilterOperator.N);
        Name = Field<string[]>("name");
    }

    public FilterField<ExtrasCustomFilterBuilder, string[]> Tag { get; }

    public FilterField<ExtrasCustomFilterBuilder, string[]> Name { get; }
}
