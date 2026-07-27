using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Core.Utility;

namespace ShoppingApp.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class HealthcheckController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Get()
    {
        return Ok(Result.Success());
    }
}
