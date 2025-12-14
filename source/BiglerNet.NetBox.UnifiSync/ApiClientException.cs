namespace BiglerNet.NetBox.UnifiSync;
public class ApiClientException : Exception
{
    public int StatusCode { get; }

    public string? ResponseContent { get; }

    public ApiClientException(string? message, int statusCode, string? responseContent)
        : base(message)
    {
        StatusCode = statusCode;
        ResponseContent = responseContent;
    }

    public ApiClientException(int statusCode, string? responseContent)
    {
        StatusCode = statusCode;
        ResponseContent = responseContent;
    }

    public ApiClientException(string? message, Exception? innerException, int statusCode, string? responseContent)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        ResponseContent = responseContent;
    }
}
