using BiglerNet.NetBox.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client.QueryFilters;

public sealed class DcimCableTerminationFilter : NetBoxQueryFilter
{
}

public class DcimCableTerminationBuilder
    : NetBoxQueryFilterBuilder<DcimCableTerminationBuilder, DcimCableTerminationFilter>
{
    public DcimCableTerminationBuilder()
    {
        Cable = Field<int>("cable", FilterOperator.N);
        CableEnd = Field<CableEnd>("cable_end");
    }

    public FilterField<DcimCableTerminationBuilder, int> Cable { get; }

    public FilterField<DcimCableTerminationBuilder, CableEnd> CableEnd { get; }
}
