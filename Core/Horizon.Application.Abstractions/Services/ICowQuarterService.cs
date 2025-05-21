using Horizon.Application.Abstractions.Common;
using Horizon.Application.Abstractions.DataTransferObjects;

namespace Horizon.Application.Abstractions.Services;
public interface ICowQuarterService
{
    Task<Result> AddQuarterAsync(string cowId, CowQuarterDto dto);
}
