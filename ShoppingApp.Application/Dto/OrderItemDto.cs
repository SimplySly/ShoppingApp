namespace ShoppingApp.Application.Dto;

public sealed record OrderItemDto(int ProductId, 
    int Quantity)
{
}
