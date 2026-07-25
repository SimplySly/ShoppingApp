namespace ShoppingApp.Application.Abstractions.Messaging;

public interface ICommand : IBaseCommand
{

}

public interface ICommand<TResponse> : IBaseCommand
{

}
