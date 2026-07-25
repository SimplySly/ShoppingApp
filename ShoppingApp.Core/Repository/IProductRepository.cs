using ShoppingApp.Core.Abstractions.Repository;
using ShoppingApp.Core.Entities;

namespace ShoppingApp.Core.Repository;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsPage(int page, int pageSize);
}
