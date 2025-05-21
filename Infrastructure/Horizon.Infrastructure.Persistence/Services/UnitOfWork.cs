using Horizon.Infrastructure.Persistence.Repositories;

namespace Horizon.Infrastructure.Persistence.Services;
internal class UnitOfWork(HorizonDbContext context)
        : IUnitOfWork
{
    private readonly HorizonDbContext _context = context;
    private readonly Dictionary<string, object> _repositories = [];
    private IDbContextTransaction _transaction;

    public IRepository<T> GetRepository<T>() where T : class
    {
        var type = typeof(T).Name;

        if (!_repositories.TryGetValue(type, out object? value))
        {
            var repo = new EFRepository<T>(_context);
            value = repo;
            _repositories[type] = value;
        }

        return (IRepository<T>)value;
    }

    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.SaveChangesAsync();
        await _transaction.CommitAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _transaction.RollbackAsync();
    }
}
