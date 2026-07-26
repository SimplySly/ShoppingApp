namespace ShoppingApp.Application.Settings;

public sealed class JwtSettings
{
    public required string SecretKey { get; init; }
    public required string Audience { get; init; }
    public required string Issuer { get; init; }
    public int ExpirationInMinutes { get; init; } = 5;
    public int RefreshTokenExpiryInDays { get; init; } = 30;
    public int ClockSkewInMinutes { get; init; } = 0;
}
