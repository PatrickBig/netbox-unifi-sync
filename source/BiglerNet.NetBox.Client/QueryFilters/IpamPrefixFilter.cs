using BiglerNet.NetBox.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client.QueryFilters;

public sealed class IpamPrefixFilter : NetBoxQueryFilter
{
}

public class IpamPrefixFilterBuilder
    : NetBoxQueryFilterBuilder<IpamPrefixFilterBuilder, IpamPrefixFilter>
{
    public IpamPrefixFilterBuilder()
    {
        Tag = Field<string[]>("tag", FilterOperator.N);
        TagId = Field<string[]>("tag_id", FilterOperator.N);
    }

    public FilterField<IpamPrefixFilterBuilder, string[]> Tag { get; }

    public FilterField<IpamPrefixFilterBuilder, string[]> TagId { get; }
}
