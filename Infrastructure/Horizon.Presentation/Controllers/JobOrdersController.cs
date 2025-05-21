using Horizon.Application.Abstractions.Common;
using Horizon.Application.Abstractions.DataTransferObjects.JobOrders;
using Horizon.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Presentation.Controllers;
[ProducesResponseType(typeof(BaseResponse<object>), 500)]
public class JobOrdersController(IJobOrderService jobOrderService)
    : BaseApiController
{


    [ProducesResponseType(typeof(BaseResponse<IEnumerable<JobOrderSummaryResponse>>), 200)]
    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<JobOrderSummaryResponse>>>> GetAll()
    {
        var result = await jobOrderService.GetAllJobOrdersAsync();
        return FromResult(result);
    }


    [ProducesResponseType(typeof(BaseResponse<JobOrderDetailsResponse>), 200)]
    [ProducesResponseType(typeof(BaseResponse<object>), 404)]
    [HttpGet("{orderCode}/details")]
    public async Task<ActionResult<BaseResponse<JobOrderDetailsResponse>>> GetDetails(string orderCode)
    {
        var result = await jobOrderService.GetJobOrderDetailsByCodeAsync(orderCode);
        return FromResult(result);
    }
}
