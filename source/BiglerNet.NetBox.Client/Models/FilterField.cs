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
            new QueryParameter(_name, Format(value).ToList()));
        return _builder;
    }

    public TBuilder Op(FilterOperator op, TValue value)
    {
        Ensure(op);
        if (value != null)
        {
            var suffix = op.GetEnumMemberValue();
            _filter.Parameters.Add(
                new QueryParameter($"{_name}__{suffix}", [.. Format(value)]));
        }

        return _builder;
    }

    // Convenience methods (optional but nice)
    public TBuilder Gt(TValue v) => Op(FilterOperator.Gt, v);
    public TBuilder Gte(TValue v) => Op(FilterOperator.Gte, v);
    public TBuilder Lt(TValue v) => Op(FilterOperator.Lt, v);
    public TBuilder Lte(TValue v) => Op(FilterOperator.Lte, v);
    public TBuilder Ne(TValue v) => Op(FilterOperator.N, v);

    public TBuilder Empty()
    {
        Ensure(FilterOperator.Empty);
        _filter.Parameters.Add(
            new QueryParameter($"{_name}__empty", new[] { "true" }));

        return _builder;
    }

    private void Ensure(FilterOperator op)
    {
        if (!_supported.Contains(op))
            throw new InvalidOperationException(
                $"Operator '{op}' is not supported for field '{_name}'.");
    }

    private static IEnumerable<string> Format(object? value)
    {
        if (value != null)
        {
            if (value is Array arr)
            {
                foreach (var v in arr)
                {
                    yield return v!.ToString()!;
                }
            }
            else
            {
                yield return value!.ToString()!;
            }
        }
    }
}
