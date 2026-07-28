using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;
using ShoppingApp.Application.Di;
using ShoppingApp.Application.Settings;
using ShoppingApp.Infrastructure.Database;
using ShoppingApp.Infrastructure.Di;
using ShoppingApp.Web.Middleware;
using System.Diagnostics;
using System.Text;

namespace ShoppingApp.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        AddComponents(builder);
        AddAuthentication(builder);
        AddLogger(builder);
        AddSwagger(builder);
        AddServices(builder);

        var app = builder.Build();

        UseMiddleware(app);

        try
        {
            Log.Information("Starting server...");
            app.Run();
            Log.Information("Stopping server...");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unexpected error occured");
        }
        finally
        {
            await Log.CloseAndFlushAsync(); // Ensure all logs written before app exits
        }
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

    private static void AddLogger(WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(Log.Logger);
    }

    private static void AddSwagger(WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ShoppingApp API",
                Description = "API for enterprise shopping application"
            });
            options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme."
            });
            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("bearer", document)] = []
            });
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
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers()
            .RequireAuthorization();
        app.UseMiddleware<GlobalExceptionHandler>();
    }
}
