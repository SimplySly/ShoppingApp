using Microsoft.AspNetCore.Identity;

namespace ShoppingApp.Core.Entities;

public sealed class Order
{
    public int Id { get; set; }
    public required string UserId { get; set; }

    public List<OrderItem> OrderItems { get; set; } = [];
    public IdentityUser? User { get; set; }
}
