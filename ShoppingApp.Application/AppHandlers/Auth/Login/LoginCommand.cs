using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.Dto;

namespace ShoppingApp.Application.AppHandlers.Auth.Login;

public sealed record LoginCommand(string email, 
    string password)
    : ICommand<LoginResponseDto>
{
}
