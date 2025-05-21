using Horizon.Application.Abstractions.DataTransferObjects.JobOrders;
using Mapster;

namespace Horizon.Application.Services;
internal class JobOrderService(IUnitOfWork unitOfWork)
    : IJobOrderService
{


    public async Task<Result<IEnumerable<JobOrderSummaryResponse>>> GetAllJobOrdersAsync()
    {
        var orders = await unitOfWork
            .GetRepository<JobOrder>()
            .ListAsync();

        if (orders is null || !orders.Any())
        {
            return Result<IEnumerable<JobOrderSummaryResponse>>.Failure(
                "No job orders found.",
                ErrorCode.NotFound
            );
        }

        //var response = orders.Adapt<IEnumerable<JobOrderSummaryResponse>>();
        var response = orders.Adapt<IEnumerable<JobOrderSummaryResponse>>();

        return Result<IEnumerable<JobOrderSummaryResponse>>.Success(response);
    }

    public async Task<Result<JobOrderDetailsResponse>> GetJobOrderDetailsByCodeAsync(string orderCode)
    {
        var spec = new JobOrderWithCowsSpecifications(orderCode);
        var order = await unitOfWork
            .GetRepository<JobOrder>()
            .FirstOrDefaultAsync(spec);

        if (order is null)
        {
            return Result<JobOrderDetailsResponse>.Failure(
                "Job order not found.",
                ErrorCode.NotFound
            );
        }

        var response = order.Adapt<JobOrderDetailsResponse>();
        return Result<JobOrderDetailsResponse>.Success(response);
    }
}
