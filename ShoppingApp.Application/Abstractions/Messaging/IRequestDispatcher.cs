using ShoppingApp.Core.Utility;

namespace ShoppingApp.Application.Abstractions.Messaging;

public interface IRequestDispatcher
{
    Task<Result<TResponse>> ExecuteQuery<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken)
        where TQuery : IQuery<TResponse>;
    Task<Result> ExecuteCommand<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : ICommand;
    Task<Result<TResponse>> ExecuteCommand<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken)
        where TCommand : ICommand<TResponse>;
}
