namespace Horizon.Application.Abstractions.DataTransferObjects.ERP;

public class WorkOrderLineDTO
{
    public string SerialId { get; set; } = null!;
    public string ItemName { get; set; } = null!;
    public string ItemId { get; set; } = null!;
    public double Weight { get; set; }
}
