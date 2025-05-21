using Horizon.Application.Abstractions.Common;
using Horizon.Application.Abstractions.DataTransferObjects.JobOrders;

namespace Horizon.Application.Abstractions.Services;
public interface IJobOrderService
{
    Task<Result<IEnumerable<JobOrderSummaryResponse>>> GetAllJobOrdersAsync();
    Task<Result<JobOrderDetailsResponse>> GetJobOrderDetailsByCodeAsync(string orderCode);
}
