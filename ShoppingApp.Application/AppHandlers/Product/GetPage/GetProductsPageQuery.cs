using ShoppingApp.Application.Abstractions.Messaging;

namespace ShoppingApp.Application.AppHandlers.Product.GetPage;

public record GetProductsPageQuery(int page,
    int pageSize) 
    : IQuery<IEnumerable<ProductDto>> 
{
}
