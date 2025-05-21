using Horizon.Application.Abstractions.Common;
using Horizon.Application.Abstractions.Common.Models;
using Horizon.Application.Abstractions.DataTransferObjects.Slaughter;

namespace Horizon.Application.Abstractions.Services;

public interface ICowSlaughterQueue
{
    void EnqueueSession(SlaughterSession session);
    Result AddQuarterToCurrent(CowQuarterDto quarter);
    Result<bool> IsCurrentCowComplete();
    Result<SlaughterSession> TryDequeueCompletedSession();
}