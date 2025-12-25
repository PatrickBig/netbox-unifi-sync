using BiglerNet.NetBox.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client.QueryFilters;

public sealed class IpamVlanFilter : NetBoxQueryFilter
{
}

public class IpamVlanFilterBuilder
    : NetBoxQueryFilterBuilder<IpamVlanFilterBuilder, IpamVlanFilter>
{
    public IpamVlanFilterBuilder()
    {
        Tag = Field<string[]>("tag", FilterOperator.N);
        TagId = Field<string[]>("tag_id", FilterOperator.N);
    }

    public FilterField<IpamVlanFilterBuilder, string[]> Tag { get; }

    public FilterField<IpamVlanFilterBuilder, string[]> TagId { get; }
}
