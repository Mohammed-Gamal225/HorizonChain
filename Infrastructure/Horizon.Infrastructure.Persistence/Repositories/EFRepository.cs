using Horizon.Infrastructure.Persistence.Specifications;

namespace Horizon.Infrastructure.Persistence.Repositories;
internal class EFRepository<T>(HorizonDbContext context)
    : IRepository<T>
    where T : class
{
    private readonly HorizonDbContext _context = context;

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        var query = SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        return await query.ToListAsync();
    }

    public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec)
    {
        var query = SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        var query = SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        return await query.CountAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}
