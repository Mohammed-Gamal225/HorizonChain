// Ignore Spelling: Dto

namespace Horizon.Application.Services;


public class CowSlaughterService(ICowSlaughterQueue queue, IUnitOfWork unitOfWork)
    : ICowSlaughterService
{
    private readonly ICowSlaughterQueue _queue = queue;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> RegisterPlannedCowAsync(PlannedCowRequest request)
    {
        var jobOrder = await _unitOfWork
            .GetRepository<JobOrder>()
            .FirstOrDefaultAsync(new JobOrderByOrderCodeSpecification(request.OrderCode));

        if (jobOrder is null)
            return Result.Failure("No matching job order found.");

        if (jobOrder.Status == JobOrderStatus.Planned)
        {
            jobOrder.Status = JobOrderStatus.InProgress;
            _unitOfWork.GetRepository<JobOrder>().Update(jobOrder);
        }

        var jobOrderCow = await _unitOfWork
            .GetRepository<JobOrderCow>()
            .FirstOrDefaultAsync(new JobOrderCowByIdSpecification(request.CowIdentifier));

        if (jobOrderCow is null)
            return Result.Failure("No matching job order cow found.");

        var slaughteredCow = jobOrderCow.Adapt<SlaughteredCow>();

        await _unitOfWork.GetRepository<SlaughteredCow>().AddAsync(slaughteredCow);
        _unitOfWork.GetRepository<JobOrderCow>().Delete(jobOrderCow);

        if (jobOrder.Cows.Count == 1)
        {
            jobOrder.Status = JobOrderStatus.Completed;
            _unitOfWork.GetRepository<JobOrder>().Update(jobOrder);
        }

        await _unitOfWork.SaveChangesAsync();

        _queue.EnqueueSession(new SlaughterSession
        {
            SlaughteredCowId = slaughteredCow.Id,
            CowIdentifier = slaughteredCow.CowIdentifier,
            Quarters = []
        });

        return Result.Success();
    }

    public async Task<Result> RegisterUnplannedCowAsync(UnplannedCowRequest request)
    {
        var cowRepo = _unitOfWork.GetRepository<SlaughteredCow>();

        var existing = await cowRepo.FirstOrDefaultAsync(
            new SlaughteredCowByIdentifierSpecification(request.CowIdentifier));

        if (existing is not null)
            return Result.Failure("Cow already registered.");

        var cow = request.Adapt<SlaughteredCow>();
        await cowRepo.AddAsync(cow);

        await _unitOfWork.SaveChangesAsync();

        _queue.EnqueueSession(new SlaughterSession
        {
            SlaughteredCowId = cow.Id,
            CowIdentifier = cow.CowIdentifier,
            Quarters = []
        });

        return Result.Success();
    }

    public async Task<Result> AddQuarterAsync(CowQuarterDto quarterDto)
    {
        var addResult = _queue.AddQuarterToCurrent(quarterDto);
        if (addResult.IsFailure)
            return addResult;

        var isComplete = _queue.IsCurrentCowComplete();
        if (isComplete.IsFailure || !isComplete.Value)
            return Result.Success();

        var dequeue = _queue.TryDequeueCompletedSession();
        if (dequeue.IsFailure)
            return Result.Failure("Failed to finalize slaughter session.");

        var session = dequeue.Value;

        var cow = await _unitOfWork.GetRepository<SlaughteredCow>()
            .GetByIdAsync(session!.SlaughteredCowId);

        if (cow == null)
            return Result.Failure("Cow not found in database.");

        cow.SlaughteredAt = DateTime.UtcNow;

        cow.Quarters = session.Quarters.Select((q, index) =>
        {
            var typeEnum = index switch
            {
                0 => CowQuarterType.FrontLeft,
                1 => CowQuarterType.HindLeft,
                2 => CowQuarterType.FrontRight,
                3 => CowQuarterType.HindRight,
                _ => throw new InvalidOperationException("Too many quarters in session.")
            };

            return new CowQuarter
            {
                Sequence = index + 1,
                QuarterType = typeEnum,
                Weight = q.Weight,
                CreatedAt = DateTime.UtcNow,
                CowIdentifier = session.CowIdentifier,
            };
        }).ToList();

        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
