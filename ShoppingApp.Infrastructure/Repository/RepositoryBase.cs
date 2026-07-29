using ShoppingApp.Core.Abstractions.Repository;
using ShoppingApp.Infrastructure.Database;

namespace ShoppingApp.Infrastructure.Repository;

public class RepositoryBase<T> : IRepository<T>
    where T : class
{
    protected readonly ShoppingAppDbContext _dbContext;

    protected RepositoryBase(ShoppingAppDbContext shoppingAppDbContext)
    {
        _dbContext = shoppingAppDbContext;
    }

    public async Task<T?> GetById(object id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().FindAsync(id, cancellationToken);
    }

    public void Add(T entity)
    {
        _dbContext.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }
}
