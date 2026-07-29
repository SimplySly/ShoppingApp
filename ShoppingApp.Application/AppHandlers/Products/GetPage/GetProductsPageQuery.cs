using ShoppingApp.Application.Abstractions.Messaging;

namespace ShoppingApp.Application.AppHandlers.Products.GetPage;

public record GetProductsPageQuery(int page,
    int pageSize) 
    : IQuery<IEnumerable<ProductDto>> 
{
}
