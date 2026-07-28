using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.Dto;

namespace ShoppingApp.Application.AppHandlers.Orders.Create;

public sealed record CreateOrderCommand(string userId,
    List<OrderItemDto> orderItems)
    : ICommand
{
}
