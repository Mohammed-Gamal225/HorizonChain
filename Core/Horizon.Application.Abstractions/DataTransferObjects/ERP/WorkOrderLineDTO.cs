namespace Horizon.Application.Abstractions.DataTransferObjects.ERP;

public class WorkOrderLineDTO
{
    public string SerialId { get; set; }
    public string ItemName { get; set; }
    public string ItemId { get; set; }
    public double Weight { get; set; }
}
