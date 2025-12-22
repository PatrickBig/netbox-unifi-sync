using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BiglerNet.NetBox.Client.Generator;

public static class EnumPostProcessor
{
    public static string ProcessEnumValues(string code)
    {
        var pattern =
        @"^(\s*)(\[System\.Runtime\.Serialization\.EnumMember\(Value = @""([^""]*)""\)\])";

        var replacement =
            "$1$2" +
            "$1[System.Text.Json.Serialization.JsonStringEnumMemberName(@\"$3\")]";

        var result = Regex.Replace(code, pattern, replacement, RegexOptions.Multiline, TimeSpan.FromMinutes(5));
        return result;
    }
}
