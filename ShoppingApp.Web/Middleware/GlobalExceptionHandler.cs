using ShoppingApp.Core.Errors;
using ShoppingApp.Core.Utility;

namespace ShoppingApp.Web.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<GlobalExceptionHandler> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            logger.LogError("Unexpected error occurred {error}", ex.Message);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(Result.Failure(GenericErrors.Generic()));
        }
    }
}
