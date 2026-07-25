using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Core.Utility;

namespace ShoppingApp.Application.Messaging;

public class RequestDispatcher : IRequestDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public RequestDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<Result<TResponse>> ExecuteQuery<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken)
        where TQuery : IQuery<TResponse>
    {
        var queryHandler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResponse>>();

        return queryHandler.Handle(query, cancellationToken);
    }

    public Task<Result> ExecuteCommand<TCommand>(TCommand command, CancellationToken cancellationToken) 
        where TCommand : ICommand
    {
        var commandHandler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        return commandHandler.Handle(command, cancellationToken);
    }

    public Task<Result<TResponse>> ExecuteCommand<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken) 
        where TCommand : ICommand<TResponse>
    {
        var commandHandler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResponse>>();

        return commandHandler.Handle(command, cancellationToken);
    }
}
