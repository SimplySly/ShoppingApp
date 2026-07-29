using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Entities;
using ShoppingApp.Core.Repository;
using ShoppingApp.Infrastructure.Database;

namespace ShoppingApp.Infrastructure.Repository.AppServices;

public sealed class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(ShoppingAppDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsPage(int page, int pageSize, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<Product> GetByName(string name, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Products            
            .SingleOrDefaultAsync(x => x.Name == name, cancellationToken);

        return result;
    }
}
