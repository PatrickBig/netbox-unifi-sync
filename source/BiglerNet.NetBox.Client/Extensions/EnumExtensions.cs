using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace BiglerNet.NetBox.Client.Extensions;

public static class EnumExtensions
{
    public static string? GetEnumMemberValue<TEnum>(this TEnum value)
        where TEnum : Enum
    {
        var attribute = typeof(TEnum)
            .GetMember(value.ToString())
            .FirstOrDefault()
            ?.GetCustomAttribute<EnumMemberAttribute>()
            ?.Value;
        return attribute;
    }
}
