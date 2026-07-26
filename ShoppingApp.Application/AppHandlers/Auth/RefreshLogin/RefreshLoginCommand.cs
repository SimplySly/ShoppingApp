using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.Dto;

namespace ShoppingApp.Application.AppHandlers.Auth.RefreshLogin;

public sealed record RefreshLoginCommand(string refreshToken)
    : ICommand<LoginResponseDto>
{
}
