namespace Horizon.Application.Services;

public class JobOrderSyncService(
    IErpApiClient erpClient,
    IUnitOfWork unitOfWork,
    IConfiguration config,
    ILogger<JobOrderSyncService> logger)
    : IJobOrderSyncService
{
    private readonly IErpApiClient _erpClient = erpClient;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IConfiguration _config = config;
    private readonly ILogger<JobOrderSyncService> _logger = logger;
    public async Task SyncJobOrdersAsync(string transDate, CancellationToken cancellationToken = default)
    {
        var finalTransDate = ResolveTransDate(transDate);
        var jobOrders = await FetchJobOrdersFromErpAsync(finalTransDate, cancellationToken);

        await PersistNewOrdersAsync(jobOrders, cancellationToken);
    }
    private static string ResolveTransDate(string input)
    {
        var egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
        var localNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, egyptTimeZone);
        return string.IsNullOrWhiteSpace(input) ? localNow.ToString("yyyy-MM-dd") : input;
    }
    private async Task<List<WorkOrderHeaderDTO>> FetchJobOrdersFromErpAsync(string date, CancellationToken ct)
    {
        var companyId = _config["ErpSettings:CompanyId"];
        var result = await _erpClient.GetWorkOrdersAsync(companyId!, date, ct);

        if (result.IsFailure)
        {
            _logger.LogError("ERP fetch failed: {Error}", result.Error);
            throw new Exception(result.Error);
        }

        var dto = result.Value!;
        _logger.LogInformation("Fetched {Count} work orders for {Date}", dto.WorkOrderHeaders?.Count ?? 0, date);

        return dto.WorkOrderHeaders ?? new();
    }
    private async Task PersistNewOrdersAsync(IEnumerable<WorkOrderHeaderDTO> headers, CancellationToken ct)
    {
        var jobOrderRepo = _unitOfWork.GetRepository<JobOrder>();

        foreach (var header in headers)
        {
            var spec = new JobOrderByOrderCodeSpecification(header.WorkOrder);
            var exists = await jobOrderRepo.FirstOrDefaultAsync(spec);

            if (exists != null)
                continue;

            var jobOrder = new JobOrder
            {
                Id = Guid.NewGuid(),
                OrderCode = header.WorkOrder,
                OrderDate = DateTime.Parse(header.TransDate),
                NumberOfCows = header.LinesResponse?.Count ?? 0,
                ClientName = header.CustmerName,
                ClientCode = header.CustmerAccount,
                CreatedAt = DateTime.Now
            };

            foreach (var line in header.LinesResponse!)
            {
                jobOrder.Cows.Add(new JobOrderCow
                {
                    Id = Guid.NewGuid(),
                    CowIdentifier = ExtractCowIdentifier(line.SerialId),
                    JobId = jobOrder.Id,
                    Type = line.ItemName,
                    TypeId = line.ItemId,
                    Weight = line.Weight,
                    OrderCode = header.WorkOrder
                });
            }
            jobOrder.OrderType = jobOrder.Cows.FirstOrDefault()!.Type;

            await jobOrderRepo.AddAsync(jobOrder);
        }

        await _unitOfWork.SaveChangesAsync();
    }
    private static string ExtractCowIdentifier(string serialId)
    {
        var match = Regex.Match(serialId, @"بدون/(\d+)|^(\d+)|^(\d+)/");


        return match.Groups[1].Success ? match.Groups[1].Value
             : match.Groups[2].Success ? match.Groups[2].Value
             : match.Groups[3].Success ? match.Groups[3].Value
             : string.Empty;
    }
}