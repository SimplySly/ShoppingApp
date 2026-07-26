using Microsoft.AspNetCore.Identity;

namespace ShoppingApp.Application.AppServices.Implementation.Auth;

public interface IAuthService
{
    Task<string> GenerateAccessToken(IdentityUser user);
    string GenerateRefreshToken();
}
