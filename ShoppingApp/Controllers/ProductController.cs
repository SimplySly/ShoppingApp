using Microsoft.AspNetCore.Mvc;

namespace ShoppingApp.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductController : ControllerBase
{
    [HttpGet("all")]
    public async Task<Result<IEnumerable<Product>>> GetAll()
    {

    }
}
