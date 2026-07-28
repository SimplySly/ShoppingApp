using ShoppingApp.Core.Utility;

namespace ShoppingApp.Core.Errors;

public static class ProductErrors
{
    public static Error ProductNotFound(int id) => new("Product.NotFound", $"Product with id {id} doesn't exist."); 
}
