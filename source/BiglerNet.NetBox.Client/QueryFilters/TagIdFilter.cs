using BiglerNet.NetBox.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client.QueryFilters;

public sealed class TagFilter : NetBoxQueryFilter
{
}

public class TagFilterBuilder
    : NetBoxQueryFilterBuilder<TagFilterBuilder, TagFilter>
{
    public TagFilterBuilder()
    {
        Tag = Field<string[]>("tag", FilterOperator.N);
        TagId = Field<string[]>("tag_id", FilterOperator.N);
    }

    public FilterField<TagFilterBuilder, string[]> Tag { get; }

    public FilterField<TagFilterBuilder, string[]> TagId { get; }
}
