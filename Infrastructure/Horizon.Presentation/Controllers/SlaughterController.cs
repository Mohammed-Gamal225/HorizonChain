// Ignore Spelling: Dto

using Horizon.Application.Abstractions.Common;
using Horizon.Application.Abstractions.DataTransferObjects.Slaughter;
using Horizon.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Horizon.Presentation.Controllers;

public class SlaughterController(ICowSlaughterService cowSlaughterService)
    : BaseApiController
{
    private readonly ICowSlaughterService _cowSlaughterService = cowSlaughterService;

    [HttpPost("planned")]
    public async Task<ActionResult<BaseResponse<object>>> RegisterPlanned([FromBody] PlannedCowRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _cowSlaughterService.RegisterPlannedCowAsync(request);
        return FromResult(result);
    }

    [HttpPost("unplanned")]
    public async Task<ActionResult<BaseResponse<object>>> RegisterUnplanned([FromBody] UnplannedCowRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _cowSlaughterService.RegisterUnplannedCowAsync(request);
        return FromResult(result);
    }

    [HttpPost("quarter")]
    public async Task<ActionResult<BaseResponse<object>>> AddQuarter([FromBody] CowQuarterDto quarterDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _cowSlaughterService.AddQuarterAsync(quarterDto);
        return FromResult(result);
    }
}
