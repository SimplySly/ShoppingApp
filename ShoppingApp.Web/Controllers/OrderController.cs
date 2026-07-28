using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.AppHandlers.Orders.Create;
using ShoppingApp.Application.Dto;
using ShoppingApp.Core.Static;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ShoppingApp.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IRequestDispatcher _requestDispatcher;

    public OrderController(IRequestDispatcher requestDispatcher)
    {
        _requestDispatcher = requestDispatcher;
    }

    [Authorize(Roles = AuthRoles.Customer)]
    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder([FromBody] List<OrderItemDto> orderItems, CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }

        var command = new CreateOrderCommand(userId, orderItems);
        var result = await _requestDispatcher.ExecuteCommand(command, cancellationToken);

        return Ok();
    }
}
