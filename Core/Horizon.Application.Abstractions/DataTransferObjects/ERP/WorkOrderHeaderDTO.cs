// Ignore Spelling: Custmer

namespace Horizon.Application.Abstractions.DataTransferObjects.ERP;

public class WorkOrderHeaderDTO
{
    public string WorkOrder { get; set; } = null!;
    public string TransDate { get; set; } = null!;
    public string CustmerAccount { get; set; } = null!;
    public string CustmerName { get; set; } = null!;
    public List<WorkOrderLineDTO> LinesResponse { get; set; } = new();
}
