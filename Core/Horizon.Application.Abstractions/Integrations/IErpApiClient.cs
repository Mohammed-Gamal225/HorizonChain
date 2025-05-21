using Horizon.Application.Abstractions.Common;
using Horizon.Application.Abstractions.DataTransferObjects.ERP;

namespace Horizon.Application.Abstractions.Integrations;
public interface IErpApiClient
{
    Task<Result<WorkOrderResponseDTO>>
        GetWorkOrdersAsync(string companyId, string transDate, CancellationToken cancellationToken = default);
}
