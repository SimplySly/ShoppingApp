using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.Dto;

namespace ShoppingApp.Application.AppHandlers.Products.Create;

public sealed record CreateProductCommand(string Name,
    int Sku,
    double Price)
    : ICommand<CreateEntityResponseDto>
{
}
