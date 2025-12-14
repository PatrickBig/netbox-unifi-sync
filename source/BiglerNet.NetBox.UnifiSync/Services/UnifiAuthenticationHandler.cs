using BiglerNet.NetBox.UnifiSync.Models.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.Text.Json;

namespace BiglerNet.NetBox.UnifiSync.Services;
public class UnifiAuthenticationHandler : DelegatingHandler
{
    private readonly HttpClient _httpClient;
    private readonly UnifiOptions _unifiOptions;
    private readonly ILogger<UnifiAuthenticationHandler> _logger;
    private readonly SemaphoreSlim _authLock = new(1, 1);
    private readonly CookieContainer _cookieContainer;
    private string? _csrfToken;

    public UnifiAuthenticationHandler(HttpClient httpClient, IOptions<UnifiOptions> unifiOptions, ILogger<UnifiAuthenticationHandler> logger, CookieContainer cookieContainer)
    {
        _unifiOptions = unifiOptions.Value;
        _logger = logger;
        _cookieContainer = cookieContainer;
        _httpClient = httpClient;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Check if the token is valid
        if (_csrfToken == null)
        {
            await AuthenticateAsync(cancellationToken);
        }



        AddAuthHeaders(request);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            // The token might have expired. Try re-authenticating once.
            _logger.LogInformation("Authentication token expired, re-authenticating...");
            await AuthenticateAsync(cancellationToken);
            AddAuthHeaders(request);
            response = await base.SendAsync(request, cancellationToken);
        }

        return response;
    }

    private void AddAuthHeaders(HttpRequestMessage request)
    {
        if (_csrfToken != null)
        {
            request.Headers.Remove("X-CSRF-Token");
            request.Headers.Add("X-CSRF-Token", _csrfToken);
        }
    }

    private async Task AuthenticateAsync(CancellationToken cancellationToken)
    {
        await _authLock.WaitAsync(cancellationToken);
        try
        {
            // Avoid double-auth in concurrent scenario
            if (_csrfToken != null)
            {
                return;
            }

            var loginPayload = new
            {
                username = _unifiOptions.Authentication.Username,
                password = _unifiOptions.Authentication.Password
            };

            var json = JsonSerializer.Serialize(loginPayload);

            var loginRequest = new HttpRequestMessage(HttpMethod.Post, $"{_unifiOptions.BaseUrl.TrimEnd('/')}/api/auth/login")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            using var client = new HttpClient(new HttpClientHandler
            {
                CookieContainer = _cookieContainer,
                ServerCertificateCustomValidationCallback = (_, _, _, _) => true
            });

            var response = await client.SendAsync(loginRequest, cancellationToken);
            response.EnsureSuccessStatusCode();

            // Extract CSRF
            if (response.Headers.TryGetValues("X-CSRF-Token", out var values))
            {
                _csrfToken = values.FirstOrDefault();
            }

            // Fallback: some versions put CSRF in a cookie
            var xx = _cookieContainer.GetAllCookies();
            var cookie = _cookieContainer.GetCookies(new Uri(_unifiOptions.BaseUrl.TrimEnd('/')))["csrf_token"];
            if (_csrfToken == null && cookie != null)
            {
                _csrfToken = cookie.Value;
            }

            if (_csrfToken == null)
            {
                throw new InvalidOperationException("Could not retrieve CSRF token from UniFi OS authentication.");
            }
        }
        finally
        {
            _authLock.Release();
        }
    }
}
