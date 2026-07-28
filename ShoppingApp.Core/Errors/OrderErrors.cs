using ShoppingApp.Core.Utility;

namespace ShoppingApp.Core.Errors;

public class OrderErrors
{
    public static Error ProductOutOfStock(string productName) => new("Order.OutOfStock", $"Product with name {productName} is out of stock");
}
