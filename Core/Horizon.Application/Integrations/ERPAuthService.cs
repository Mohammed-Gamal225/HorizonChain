namespace Horizon.Application.Integrations;
public class ERPAuthService(HttpClient httpClient,
    IOptions<ErpAuthOptions> options,
    ILogger<ERPAuthService> logger)
    : IERPAuthService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<ERPAuthService> _logger = logger;
    private readonly ErpAuthOptions _authOptions = options.Value;

    public async Task<Result<string>> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        var requestData = new Dictionary<string, string>
        {
            ["client_id"] = _authOptions.ClientId,
            ["client_secret"] = _authOptions.ClientSecret,
            ["grant_type"] = "client_credentials",
            ["scope"] = _authOptions.Scope
        };

        var request = new HttpRequestMessage(HttpMethod.Post, _authOptions.TokenUrl)
        {
            Content = new FormUrlEncodedContent(requestData)
        };

        try
        {
            var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError("ERP Auth failed: {Status} - {Error}", response.StatusCode, error);
                return Result<string>.Failure("Token request failed");
            }

            var content = await response.Content.ReadAsStringAsync();
            return ParseToken(content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception while requesting ERP token");
            return Result<string>.Failure("Unexpected error while requesting token.");
        }
    }

    private static Result<string> ParseToken(string json)
    {
        var obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        if (obj != null && obj.TryGetValue("access_token", out var token))
        {
            return Result<string>.Success(token?.ToString()!);
        }

        return Result<string>.Failure("access_token not found in ERP response.");
    }
}
