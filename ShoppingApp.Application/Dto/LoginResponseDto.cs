namespace ShoppingApp.Application.Dto;

public sealed record LoginResponseDto(string accessToken,
    string refreshToken)
{
}
