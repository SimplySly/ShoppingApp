using ShoppingApp.Application.Abstractions.Messaging;

namespace ShoppingApp.Application.AppHandlers.Products.Delete;

public sealed record DeleteProductCommand(int Id) : ICommand
{
}
