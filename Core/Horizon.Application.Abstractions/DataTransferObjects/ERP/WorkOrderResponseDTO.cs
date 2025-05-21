using Newtonsoft.Json;

namespace Horizon.Application.Abstractions.DataTransferObjects.ERP;

public class WorkOrderResponseDTO
{
    public string Status { get; set; }
    public string ErrorMessage { get; set; }
    [JsonProperty("Sha_headerResponse")]
    public List<WorkOrderHeaderDTO> WorkOrderHeaders { get; set; } = [];
}
