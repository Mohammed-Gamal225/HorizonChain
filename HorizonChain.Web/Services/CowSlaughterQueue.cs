using Horizon.Application.Abstractions.Common;
using Horizon.Application.Abstractions.Common.Models;
using Horizon.Application.Abstractions.DataTransferObjects.Slaughter;
using Horizon.Application.Abstractions.Services;

namespace HorizonChain.Web.Services;

public class CowSlaughterQueue : ICowSlaughterQueue
{
    private readonly Queue<SlaughterSession> _queue = new();
    private readonly object _lock = new();

    public void EnqueueSession(SlaughterSession session)
    {
        lock (_lock)
        {
            _queue.Enqueue(session);
        }
    }

    public Result AddQuarterToCurrent(CowQuarterDto quarter)
    {
        lock (_lock)
        {
            if (_queue.Count == 0)
                return Result.Failure("No active cow session in queue.");

            var current = _queue.Peek();

            if (current.Quarters.Count >= 4)
                return Result.Failure("Current cow already has 4 quarters.");

            current.Quarters.Add(quarter);
            return Result.Success();
        }
    }

    public Result<bool> IsCurrentCowComplete()
    {
        lock (_lock)
        {
            if (_queue.Count == 0)
                return Result<bool>.Success(false);

            return Result<bool>.Success(_queue.Peek().Quarters.Count == 4);
        }
    }

    public Result<SlaughterSession> TryDequeueCompletedSession()
    {
        lock (_lock)
        {
            if (_queue.Count == 0)
                return Result<SlaughterSession>.Failure("Queue is empty.");

            var current = _queue.Peek();
            if (current.Quarters.Count < 4)
                return Result<SlaughterSession>.Failure("Cow is not yet complete.");

            return Result<SlaughterSession>.Success(_queue.Dequeue());
        }
    }
}
