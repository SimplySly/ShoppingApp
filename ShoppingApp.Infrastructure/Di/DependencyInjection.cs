using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Core.Abstractions.Repository;
using ShoppingApp.Core.Repository;
using ShoppingApp.Infrastructure.Database;
using ShoppingApp.Infrastructure.Repository;
using ShoppingApp.Infrastructure.Repository.AppServices;

namespace ShoppingApp.Infrastructure.Di;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ShoppingAppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ShoppingAppDb"));
        });

        services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IAuthRepository, AuthRepository>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
