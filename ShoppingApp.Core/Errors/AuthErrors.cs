using Microsoft.AspNetCore.Identity;
using ShoppingApp.Core.Utility;

namespace ShoppingApp.Core.Errors;

public static class AuthErrors
{

    public static Error UserAlreadyExistsError(string email) => new("Auth.UserAlreadyExists", $"A user with the email {email} already exists.");

    public static Error UserCreationError(IEnumerable<IdentityError> errors) =>
        new("Auth.UserCreationError", $"Failed to create user.\n{string.Join("\n", errors.Select(e => e.Description))}");

    public static Error UserNotFound() => new("Auth.UserNotFound", "User not found.");

    public static Error InvalidCredentials() => new("Auth.InvalidCredentials", "Invalid login credentials.");
}
