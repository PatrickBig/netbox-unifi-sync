using BiglerNet.NetBox.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client;

public interface INetBoxQuery
{
    public string ToQueryString<TFilter>(TFilter filter)
        where TFilter : class;
}
