using ShoppingApp.Application.Abstractions.Messaging;

namespace ShoppingApp.Application.AppHandlers.Auth.Register;

public record RegisterCommand (string Username, 
    string Email, 
    string Password)
    : ICommand
{
}
