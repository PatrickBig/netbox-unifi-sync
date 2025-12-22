using BiglerNet.NetBox.Client.Models;

namespace BiglerNet.NetBox.Client.QueryFilters;

public sealed class ExtrasCustomFieldFilter : NetBoxQueryFilter
{
}

public class ExtrasCustomFieldFilterBuilder
    : NetBoxQueryFilterBuilder<ExtrasCustomFieldFilterBuilder, ExtrasCustomFieldFilter>
{
    public ExtrasCustomFieldFilterBuilder()
    {

        Tag = Field<string[]>("tag", FilterOperator.N);
        Name = Field<string[]>("name");
    }

    public FilterField<ExtrasCustomFieldFilterBuilder, string[]> Tag { get; }

    public FilterField<ExtrasCustomFieldFilterBuilder, string[]> Name { get; }
}
