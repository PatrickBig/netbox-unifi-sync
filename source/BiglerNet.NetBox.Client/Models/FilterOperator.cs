using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BiglerNet.NetBox.Client.Models;

public enum FilterOperator
{
    [EnumMember(Value = "gt")]
    Gt,
    
    [EnumMember(Value = "gte")]
    Gte,

    [EnumMember(Value = "lt")]
    Lt,

    [EnumMember(Value = "lte")]
    Lte,

    [EnumMember(Value = "n")]
    N,

    [EnumMember(Value = "empty")]
    Empty,

    [EnumMember(Value = "ic")]
    Ic,

    [EnumMember(Value = "ie")]
    Ie,

    [EnumMember(Value = "iew")]
    Iew,

    [EnumMember(Value = "iregex")]
    Iregex,

    [EnumMember(Value = "isw")]
    Isw,

    [EnumMember(Value = "nic")]
    Nic,

    [EnumMember(Value = "nie")]
    Nie,

    [EnumMember(Value = "niew")]
    Niew,

    [EnumMember(Value = "nisw")]
    Nisw,

    [EnumMember(Value = "regex")]
    Regex,
}