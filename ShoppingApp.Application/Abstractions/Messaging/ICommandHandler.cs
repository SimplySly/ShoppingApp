using ShoppingApp.Core.Utility;

namespace ShoppingApp.Application.Abstractions.Messaging;

public interface ICommandHandler <TCommand> 
    where TCommand : ICommand
{
    Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}