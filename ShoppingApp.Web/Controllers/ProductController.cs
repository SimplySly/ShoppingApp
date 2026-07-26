using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Application;
using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.AppHandlers.Product.GetPage;

namespace ShoppingApp.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IRequestDispatcher _requestDispatcher;

    public ProductController(IRequestDispatcher requestDispatcher)
    {
        _requestDispatcher = requestDispatcher;
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetProductList([FromQuery] int page, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetProductsPageQuery(page, pageSize);
        var result = await _requestDispatcher.ExecuteQuery<GetProductsPageQuery, IEnumerable<ProductDto>>(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }
}
