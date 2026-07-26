using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.AppHandlers.Auth.Login;
using ShoppingApp.Application.AppHandlers.Auth.RefreshLogin;
using ShoppingApp.Application.AppHandlers.Auth.Register;
using ShoppingApp.Application.AppHandlers.Auth.Roles.GetRoles;
using ShoppingApp.Application.Dto;
using ShoppingApp.Core.Static;

namespace ShoppingApp.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IRequestDispatcher _requestDispatcher;

    public AuthController(IRequestDispatcher requestDispatcher)
    {
        _requestDispatcher = requestDispatcher;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await _requestDispatcher.ExecuteCommand(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await _requestDispatcher.ExecuteCommand<LoginCommand, LoginResponseDto>(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }

    [HttpPost("refresh-login")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshLogin([FromBody] RefreshLoginCommand command, CancellationToken cancellationToken)
    {
        var result = await _requestDispatcher.ExecuteCommand<RefreshLoginCommand, LoginResponseDto>(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }

    [HttpGet("roles")]
    [Authorize(Roles = AuthRoles.Admin)]
    public async Task<IActionResult> GetRoles(CancellationToken cancellationToken)
    {
        var query = new GetRolesQuery();
        var result = await _requestDispatcher.ExecuteQuery<GetRolesQuery, IEnumerable<AuthRoleDto>>(query, cancellationToken);

        return Ok(result);
    }
}
