using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShoppingApp.Application.Di;
using ShoppingApp.Application.Settings;
using ShoppingApp.Infrastructure.Database;
using ShoppingApp.Infrastructure.Di;
using System.Diagnostics;
using System.Text;

namespace ShoppingApp.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        AddComponents(builder);
        AddAuthentication(builder);
        AddServices(builder);

        var app = builder.Build();

        UseMiddleware(app);

        app.Run();
    }

    public static void AddComponents(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
    }

    private static void AddAuthentication(WebApplicationBuilder builder)
    {
        builder.Services
            .AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ShoppingAppDbContext>();

        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>() 
                    ?? throw new InvalidOperationException("Missing JWT settings section in configuration");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ClockSkew = TimeSpan.FromMinutes(jwtSettings.ClockSkewInMinutes)
                };
            });
    }

    private static void AddServices(WebApplicationBuilder builder)
    {
        builder.Services
            .AddApplicationLayer(builder.Configuration)
            .AddInfrastructureLayer(builder.Configuration);
    }

    public static void UseMiddleware(WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}
