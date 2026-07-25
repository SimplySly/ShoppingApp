using ShoppingApp.Core.Abstractions.Repository;
using ShoppingApp.Infrastructure.Database;

namespace ShoppingApp.Infrastructure.Repository;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ShoppingAppDbContext _dbContext;

    public UnitOfWork(ShoppingAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
