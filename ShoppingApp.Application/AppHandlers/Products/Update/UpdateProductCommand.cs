using ShoppingApp.Application.Abstractions.Messaging;

namespace ShoppingApp.Application.AppHandlers.Products.Update;

public sealed record UpdateProductCommand(int Id,
    string Name,
    int Sku,
    double Price)
    : ICommand
{
}
