using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BiglerNet.NetBox.Client;

public class NetBoxApiClientException : Exception
{
    public int StatusCode { get; }
    public string? Content { get; }

    public NetBoxApiClientException(int statusCode, string? content, string message)
        : base(message)
    {
        StatusCode = statusCode;
        Content = content;
    }

    public NetBoxApiClientException(int statusCode, string? content)
    {
        StatusCode = statusCode;
        Content = content;
    }

    public NetBoxApiClientException(HttpStatusCode statusCode, string? content, string message)
        : base(message)
    {
        StatusCode = (int)statusCode;
        Content = content;
    }

    public NetBoxApiClientException(HttpStatusCode statusCode, string? content)
    {
        StatusCode = (int)statusCode;
        Content = content;
    }
}
