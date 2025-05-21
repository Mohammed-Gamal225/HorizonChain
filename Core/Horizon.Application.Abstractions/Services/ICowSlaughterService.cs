using Horizon.Application.Abstractions.Common;
using Horizon.Application.Abstractions.DataTransferObjects.Slaughter;

namespace Horizon.Application.Abstractions.Services;
public interface ICowSlaughterService
{
    Task<Result> RegisterUnplannedCowAsync(UnplannedCowRequest request);
    Task<Result> RegisterPlannedCowAsync(PlannedCowRequest request);
    Task<Result> AddQuarterAsync(CowQuarterDto quarter);
}
