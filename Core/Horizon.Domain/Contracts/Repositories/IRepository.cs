using Horizon.Domain.Contracts.Specifications;

namespace Horizon.Domain.Contracts.Repositories;
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> ListAsync();
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task<T?> FirstOrDefaultAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);

    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
