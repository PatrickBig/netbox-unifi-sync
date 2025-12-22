using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BiglerNet.NetBox.Client;

public class NetBoxApiClientException : Exception
{
    public int StatusCode { get; }
    public string? Content { get; }

    public NetBoxApiClientException(int statusCode, string? content, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        Content = content;
    }

    public NetBoxApiClientException(int statusCode, string? content, Exception? innerException = null)
        : base("An unexpected exception occured when calling the NetBox API.", innerException)
    {
        StatusCode = statusCode;
        Content = content;
    }

    public NetBoxApiClientException(HttpStatusCode statusCode, string? content, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        StatusCode = (int)statusCode;
        Content = content;
    }

    public NetBoxApiClientException(HttpStatusCode statusCode, string? content, Exception? innerException = null)
        : base("An unexpected exception occured when calling the NetBox API.", innerException)
    {
        StatusCode = (int)statusCode;
        Content = content;
    }
}
