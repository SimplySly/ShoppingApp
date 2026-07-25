using ShoppingApp.Application.Di;
using ShoppingApp.Infrastructure.Di;

namespace ShoppingApp.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        AddComponents(builder);
        AddServices(builder);

        var app = builder.Build();

        UseMiddleware(app);

        app.Run();
    }

    public static void AddComponents(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
    }

    private static void AddServices(WebApplicationBuilder builder)
    {
        builder.Services
            .AddApplicationLayer()
            .AddInfrastructureLayer(builder.Configuration);
    }

    public static void UseMiddleware(WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}
