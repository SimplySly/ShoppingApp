using ShoppingApp.Core.Entities;
using ShoppingApp.Core.Repository;
using ShoppingApp.Infrastructure.Database;

namespace ShoppingApp.Infrastructure.Repository.AppServices;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(ShoppingAppDbContext dbContext) 
        : base(dbContext)
    {
    }
}
