namespace Horizon.Application.Integrations;

public class ErpApiClient(HttpClient httpClient,
    IERPAuthService authService,
    ILogger<ErpApiClient> logger,
    IConfiguration config)
    : IErpApiClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IERPAuthService _authService = authService;
    private readonly ILogger<ErpApiClient> _logger = logger;
    private readonly IConfiguration _config = config;

    public async Task<Result<WorkOrderResponseDTO>> GetWorkOrdersAsync(string companyId, string transDate, CancellationToken cancellationToken)
    {
        var tokenResult = await _authService.GetAccessTokenAsync(cancellationToken);
        if (tokenResult.IsFailure)
            return Result<WorkOrderResponseDTO>.Failure(tokenResult.Error!);

        var token = tokenResult.Value!;
        var payload = new { CompanyId = companyId, TransDate = transDate };
        var endpoint = _config["ErpSettings:WorkOrderUrl"];

        var request = BuildRequest(endpoint!, token, payload);
        var response = await SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogWarning("ERP call failed: {Code} - {Error}", response.StatusCode, errorContent);
            return Result<WorkOrderResponseDTO>.Failure($"ERP call failed: {response.StatusCode}");
        }
        var dto = await DeserializeResponse<WorkOrderResponseDTO>(response);
        return dto is not null
            ? Result<WorkOrderResponseDTO>.Success(dto)
            : Result<WorkOrderResponseDTO>.Failure("Failed to deserialize ERP response.");
    }

    private static HttpRequestMessage BuildRequest(string url, string token, object payload)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        return request;
    }

    private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending ERP request to {Url}", request.RequestUri);
        return await _httpClient.SendAsync(request, cancellationToken);
    }

    private async Task<T?> DeserializeResponse<T>(HttpResponseMessage response)
    {
        var raw = await response.Content.ReadAsStringAsync();
        //Console.WriteLine(raw);
        try
        {
            return JsonConvert.DeserializeObject<T>(raw);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to deserialize ERP response: {Raw}", raw);
            return default;
        }

    }
}
