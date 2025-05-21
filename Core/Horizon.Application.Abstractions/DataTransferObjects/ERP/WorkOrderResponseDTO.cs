using Newtonsoft.Json;

namespace Horizon.Application.Abstractions.DataTransferObjects.ERP;

public class WorkOrderResponseDTO
{
    public string Status { get; set; } = null!;
    public string ErrorMessage { get; set; } = null!;
    [JsonProperty("Sha_headerResponse")]
    public List<WorkOrderHeaderDTO> WorkOrderHeaders { get; set; } = [];
}
