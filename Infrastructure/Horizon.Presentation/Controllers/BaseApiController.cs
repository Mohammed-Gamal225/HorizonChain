// Ignore Spelling: Api

using Horizon.Application.Abstractions.Common;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected ActionResult<BaseResponse<T>> FromResult<T>(Result<T> result)
    {
        var response = BaseResponse<T>.FromResult(result);

        return result.IsSuccess
            ? Ok(response)
            : response.Code switch
            {
                400 => BadRequest(response),
                401 => Unauthorized(),
                404 => NotFound(response),
                409 => Conflict(response),
                _ => StatusCode(response.Code, response)
            };
    }


    protected ActionResult<BaseResponse<object>> FromResult(Result result)
    {
        var response = BaseResponse<object>.FromResult(result);

        return result.IsSuccess
            ? Ok(response)
            : response.Code switch
            {
                400 => BadRequest(response),
                401 => Unauthorized(),
                404 => NotFound(response),
                409 => Conflict(response),
                _ => StatusCode(response.Code, response)
            };
    }
}
