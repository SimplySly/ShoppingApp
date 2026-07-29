using ShoppingApp.Core.Utility;

namespace ShoppingApp.Core.Errors;

public static class ProductErrors
{
    public static Error ProductNotFound(int id) => new("Product.NotFound", $"Product with id {id} doesn't exist.");
    public static Error ProductAlreadyExists(string name) => new("Product.Exists", $"Product with name {name} already exists.");
    public static Error InvalidSku() => new("Product.InvalidSku", "Invalid SKU supplied.");
    public static Error InvalidPrice() => new("Product.InvalidPrice", "Invalid price supplied.");
}
