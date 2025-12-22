using BiglerNet.NetBox.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client.QueryFilters;

public sealed class ExtrasTagFilter : NetBoxQueryFilter
{
}

public class ExtrasTagFilterBuilder
    : NetBoxQueryFilterBuilder<ExtrasTagFilterBuilder, ExtrasTagFilter>
{
    public ExtrasTagFilterBuilder()
    {
        TagSlug = Field<string[]>("slug", FilterOperator.Empty, FilterOperator.Ic, FilterOperator.Ic, FilterOperator.Ie, FilterOperator.Iew, FilterOperator.Iregex, FilterOperator.Isw, FilterOperator.N, FilterOperator.Nic, FilterOperator.Nie, FilterOperator.Niew, FilterOperator.Nisw, FilterOperator.Regex);
    }

    public FilterField<ExtrasTagFilterBuilder, string[]> TagSlug { get; }
}
