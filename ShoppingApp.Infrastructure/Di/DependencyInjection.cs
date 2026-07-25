using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Core.Repository;
using ShoppingApp.Infrastructure.Database;
using ShoppingApp.Infrastructure.Repository.Products;

namespace ShoppingApp.Infrastructure.Di;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ShoppingAppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ShoppingAppDb"));
        });

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
