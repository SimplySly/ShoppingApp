namespace ShoppingApp.Core.Entities;

public sealed class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Sku { get; set; }
    public double Price { get; set; }
}
