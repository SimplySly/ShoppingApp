using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.AppServices.Implementation.Auth;
using ShoppingApp.Application.AppServices.Interface.Auth;
using ShoppingApp.Application.Messaging;
using ShoppingApp.Application.Settings;

namespace ShoppingApp.Application.Di;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = typeof(DependencyInjection).Assembly;

        services.AddScoped<IRequestDispatcher, RequestDispatcher>();
        services.AddScoped<IAuthService, AuthService>();

        services.Scan(scan => scan.FromAssemblies(currentAssembly)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        services.Scan(scan => scan.FromAssemblies(currentAssembly)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        services.Scan(scan => scan.FromAssemblies(currentAssembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        return services;
    }
}
