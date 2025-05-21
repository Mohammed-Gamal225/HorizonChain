namespace Horizon.Application.Abstractions.DataTransferObjects.JobOrders;
public record JobOrderSummaryResponse
{
    public string OrderCode { get; init; } = default!;
    public DateTime OrderDate { get; init; } = default!;
    public string ClientName { get; init; } = default!;
    public string ClientCode { get; init; } = default!;
}
