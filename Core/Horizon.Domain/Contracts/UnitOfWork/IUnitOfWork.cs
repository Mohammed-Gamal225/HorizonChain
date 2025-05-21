using Horizon.Domain.Contracts.Repositories;

namespace Horizon.Domain.Contracts.UnitOfWork;
public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;

    Task<int> SaveChangesAsync();
    //Task BeginTransactionAsync();
    //Task CommitTransactionAsync();
    //Task RollbackTransactionAsync();
}
