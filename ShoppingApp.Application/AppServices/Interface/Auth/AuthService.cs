using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShoppingApp.Application.AppServices.Implementation.Auth;
using ShoppingApp.Application.Settings;
using ShoppingApp.Core.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ShoppingApp.Application.AppServices.Interface.Auth;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IAuthRepository _authRepository;

    public AuthService(IOptions<JwtSettings> jwtSettings, 
        IAuthRepository authRepository)
    {
        _jwtSettings = jwtSettings.Value;
        _authRepository = authRepository;
    }

    public async Task<string> GenerateAccessToken(IdentityUser user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims(user);

        var jwt = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            signingCredentials: signingCredentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        var accessToken = tokenHandler.WriteToken(jwt);

        return accessToken;
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims(IdentityUser user)
    {
        var roles = await _authRepository.GetUserRoles(user);

        var ret = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
        };

        foreach (var role in roles)
        {
            ret.Add(new Claim(ClaimTypes.Role, role));
        }

        return ret;
    }
}
