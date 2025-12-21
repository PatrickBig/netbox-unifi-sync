using BiglerNet.NetBox.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client.QueryFilters;

public abstract class NetBoxQueryFilterBuilder<TBuilder, TFilter> : NetBoxQueryFilterBuilderBase
    where TBuilder : NetBoxQueryFilterBuilder<TBuilder, TFilter>
    where TFilter : NetBoxQueryFilter, new()
{
    protected readonly TFilter Filter = new();
    protected TBuilder Self => (TBuilder)this;


    public TFilter Build() => Filter;

    public TBuilder Limit(int limit)
    {
        Filter.Limit = limit;
        return Self;
    }

    public TBuilder Offset(int? offset)
    {
        Filter.Offset = offset;
        return Self;
    }

    // 🔑 Centralized field creation
    protected FilterField<TBuilder, TValue> Field<TValue>(
        string name,
        params FilterOperator[] supportedOperators)
    {
        return new FilterField<TBuilder, TValue>(
            Self,
            Filter,
            name,
            supportedOperators);
    }
}

public abstract class NetBoxQueryFilterBuilderBase
{
}