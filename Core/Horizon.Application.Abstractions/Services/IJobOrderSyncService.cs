namespace Horizon.Application.Abstractions.Services;
public interface IJobOrderSyncService
{
    Task SyncJobOrdersAsync(string transDate, CancellationToken cancellationToken = default);
}
