namespace Horizon.Application.Abstractions.DataTransferObjects.JobOrders;
public record JobOrderDetailsResponse
{
    public string Code { get; set; }
    public string Type { get; set; }
    public List<CowResponse> Cows { get; set; } = [];
}
public record CowResponse
{
    public string Code { get; set; }
    public double Weight { get; set; }
}
