using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Entities;
using ShoppingApp.Core.Repository;
using ShoppingApp.Infrastructure.Database;

namespace ShoppingApp.Infrastructure.Repository.AppServices;

public sealed class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    private readonly ShoppingAppDbContext _dbContext;

    public ProductRepository(ShoppingAppDbContext dbContext) 
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetProductsPage(int page, int pageSize, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return result;
    }
}
