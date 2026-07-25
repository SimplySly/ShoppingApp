using ShoppingApp.Application.Abstractions.Messaging;

namespace ShoppingApp.Application.Product.GetPage;

public record GetProductsPageQuery(int page,
    int pageSize) 
    : IQuery<IEnumerable<ProductDto>> 
{
}
