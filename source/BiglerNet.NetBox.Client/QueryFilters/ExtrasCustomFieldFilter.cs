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
        GroupName = Field<string[]>("group_name", FilterOperator.Empty, FilterOperator.Ic, FilterOperator.Ie, FilterOperator.Iew, FilterOperator.Iregex, FilterOperator.Isw, FilterOperator.N, FilterOperator.Nic, FilterOperator.Nie, FilterOperator.Niew, FilterOperator.Nisw, FilterOperator.Regex);
    }

    public FilterField<ExtrasCustomFieldFilterBuilder, string[]> Tag { get; }

    public FilterField<ExtrasCustomFieldFilterBuilder, string[]> Name { get; }

    public FilterField<ExtrasCustomFieldFilterBuilder, string[]> GroupName { get; }
}
