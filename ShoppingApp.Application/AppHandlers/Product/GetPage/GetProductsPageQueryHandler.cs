using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Core.Errors;
using ShoppingApp.Core.Repository;
using ShoppingApp.Core.Utility;

namespace ShoppingApp.Application.AppHandlers.Product.GetPage;

public sealed class GetProductsPageQueryHandler : IQueryHandler<GetProductsPageQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsPageQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository; 
    }

    public async Task<Result<IEnumerable<ProductDto>>> Handle(GetProductsPageQuery query, CancellationToken cancellationToken)
    {
        if (query.page < 1)
        {
            return Result.Failure<IEnumerable<ProductDto>>(GenericErrors.InvalidParam(nameof(query.page)));
        }

        if (query.pageSize < 1)
        {
            return Result.Failure<IEnumerable<ProductDto>>(GenericErrors.InvalidParam(nameof(query.pageSize)));
        }

        var dbResult = await _productRepository
            .GetProductsPage(query.page, query.pageSize, cancellationToken);

        // Map the result to ProductDto
        var result = dbResult
            .Select(p => 
                new ProductDto(p.Id, 
                p.Name,
                p.Sku, 
                p.Price));

        return Result.Success(result);
    }
}
