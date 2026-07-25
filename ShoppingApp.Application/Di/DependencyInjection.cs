using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.Messaging;

namespace ShoppingApp.Application.Di;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        var currentAssembly = typeof(DependencyInjection).Assembly;

        services.AddScoped<IRequestDispatcher, RequestDispatcher>();

        services.Scan(scan => scan.FromAssemblies(currentAssembly)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        services.Scan(scan => scan.FromAssemblies(currentAssembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
