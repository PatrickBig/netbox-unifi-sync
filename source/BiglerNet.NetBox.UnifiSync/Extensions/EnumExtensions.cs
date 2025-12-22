using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace BiglerNet.NetBox.UnifiSync.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Maps two different enum values together using matching <see cref="JsonStringEnumMemberNameAttribute"/> values to determine the match.
    /// </summary>
    /// <typeparam name="TDestinationEnum"></typeparam>
    /// <typeparam name="TSourceEnum"></typeparam>
    /// <param name="sourceEnum"></param>
    /// <returns></returns>
    public static TDestinationEnum? GetMatchingEnumValue<TDestinationEnum, TSourceEnum>(this TSourceEnum? sourceEnum)
        where TDestinationEnum : struct, Enum
        where TSourceEnum : Enum
    {
        // Using the JsonStringEnumMemberNameAttribute find the matching enum value in the destination enum
        if (sourceEnum != null)
        {
            var sourceEnumType = typeof(TSourceEnum);
            var destinationEnumType = typeof(TDestinationEnum);
            
            // Get the JsonStringEnumMemberNameAttribute value from the source enum
            var sourceMemberInfo = sourceEnumType.GetMember(sourceEnum.ToString())[0];
            var sourceAttribute = sourceMemberInfo.GetCustomAttribute<JsonStringEnumMemberNameAttribute>();
            
            if (sourceAttribute != null)
            {
                // Iterate through destination enum values to find a match
                foreach (var destinationValue in Enum.GetValues(destinationEnumType))
                {
                    if (destinationValue != null)
                    {
                        var destinationValueMemberName = destinationValue.ToString();
                        var destinationMemberInfo = destinationEnumType.GetMember(destinationValueMemberName!)[0];
                        var destinationAttribute = destinationMemberInfo.GetCustomAttribute<JsonStringEnumMemberNameAttribute>();
                    
                        if (destinationAttribute != null && 
                            string.Equals(sourceAttribute.Name, destinationAttribute.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            return (TDestinationEnum)destinationValue;
                        }
                    }
                }
            }
        }

        return null;
    }
}
