using BiglerNet.NetBox.Client.Extensions;
using BiglerNet.NetBox.Client.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client.Models;

public sealed class FilterField<TBuilder, TValue>
    where TBuilder : NetBoxQueryFilterBuilderBase
{
    private readonly TBuilder _builder;
    private readonly NetBoxQueryFilter _filter;
    private readonly string _name;
    private readonly HashSet<FilterOperator> _supported;

    internal FilterField(
        TBuilder builder,
        NetBoxQueryFilter filter,
        string name,
        params FilterOperator[] supportedOperators)
    {
        _builder = builder;
        _filter = filter;
        _name = name;
        _supported = supportedOperators.ToHashSet();
    }

    /// <summary>
    /// Value is equal to.
    /// </summary>
    /// <param name="value">Value to match.</param>
    /// <returns></returns>
    public TBuilder Eq(TValue value)
    {
        _filter.Parameters.Add(
            new QueryParameter(_name, new[] { Format(value) }));

        return _builder;
    }

    public void Eq(params TValue[] values)
    {
        _filter.Parameters.Add(
            new QueryParameter(_name, values.Select(Format).ToList()));
    }

    public void Op(FilterOperator op, TValue value)
    {
        Ensure(op);

        var suffix = op.GetEnumMemberValue();
        _filter.Parameters.Add(
            new QueryParameter($"{_name}__{suffix}", new[] { Format(value) }));
    }

    // Convenience methods (optional but nice)
    public void Gt(TValue v) => Op(FilterOperator.Gt, v);
    public void Gte(TValue v) => Op(FilterOperator.Gte, v);
    public void Lt(TValue v) => Op(FilterOperator.Lt, v);
    public void Lte(TValue v) => Op(FilterOperator.Lte, v);
    public void Ne(TValue v) => Op(FilterOperator.N, v);

    public void Empty()
    {
        Ensure(FilterOperator.Empty);
        _filter.Parameters.Add(
            new QueryParameter($"{_name}__empty", new[] { "true" }));
    }

    private void Ensure(FilterOperator op)
    {
        if (!_supported.Contains(op))
            throw new InvalidOperationException(
                $"Operator '{op}' is not supported for field '{_name}'.");
    }

    private static string Format(TValue value)
        => value!.ToString()!;
}
