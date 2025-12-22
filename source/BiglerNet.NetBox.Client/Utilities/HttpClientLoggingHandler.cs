using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BiglerNet.NetBox.Client.Utilities;

public class HttpClientLoggingHandler : DelegatingHandler
{
    private readonly ILogger<HttpClientLoggingHandler> _logger;

    public HttpClientLoggingHandler(ILogger<HttpClientLoggingHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        StringBuilder output = new();
        
        output.AppendLine("## REQUEST ##");
        output.AppendLine($"\t{request.ToString()}");
        var requestContent = request.Content != null ? await request.Content.ReadAsStringAsync(cancellationToken) : "<no content>";
        output.AppendLine($"\tRequest Content: {requestContent}");

        var result = await base.SendAsync(request, cancellationToken);

        output.AppendLine("## RESPONSE ##");
        output.AppendLine($"\tHTTP response code: {result.StatusCode}");
        output.AppendLine($"\t{result.ToString()})");
        var responseBody = await result.Content.ReadAsStringAsync(cancellationToken);
        output.AppendLine($"Response Body: {responseBody}");

        if (!result.IsSuccessStatusCode)
        {
            _logger.LogError(output.ToString());
        }

        return result;
    }
}
