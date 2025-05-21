namespace Horizon.Application.Abstractions.DataTransferObjects.ERP;

public class WorkOrderHeaderDTO
{
    public string WorkOrder { get; set; }
    public string TransDate { get; set; }
    public string CustmerAccount { get; set; }
    public string CustmerName { get; set; }
    public List<WorkOrderLineDTO> LinesResponse { get; set; } = new();
}
