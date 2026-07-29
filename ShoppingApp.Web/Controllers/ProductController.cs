using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Application;
using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.AppHandlers.Products.Create;
using ShoppingApp.Application.AppHandlers.Products.Delete;
using ShoppingApp.Application.AppHandlers.Products.GetPage;
using ShoppingApp.Application.AppHandlers.Products.Update;
using ShoppingApp.Application.Dto;
using ShoppingApp.Core.Static;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ShoppingApp.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductController : ControllerBase
{
    private readonly IRequestDispatcher _requestDispatcher;

    public ProductController(IRequestDispatcher requestDispatcher)
    {
        _requestDispatcher = requestDispatcher;
    }

    [HttpGet("list")]
    [AllowAnonymous]
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

    [HttpPost("create")]
    [Authorize(Roles = AuthRoles.Admin)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _requestDispatcher.ExecuteCommand<CreateProductCommand, CreateEntityResponseDto>(request, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPut("update")]
    [Authorize(Roles = AuthRoles.Admin)]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _requestDispatcher.ExecuteCommand(request, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpDelete("delete")]
    [Authorize(Roles = AuthRoles.Admin)]
    public async Task<IActionResult> Delete([FromBody] DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _requestDispatcher.ExecuteCommand(request, cancellationToken);

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
