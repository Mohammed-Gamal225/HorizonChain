namespace Horizon.Application.Abstractions.DataTransferObjects.JobOrders;
public record JobOrderDetailsResponse
{
    public string Code { get; set; } = null!;
    public string Type { get; set; } = null!;
    public List<CowResponse> Cows { get; set; } = [];
}
public record CowResponse
{
    public string Code { get; set; } = null!;
    public double Weight { get; set; }
}
