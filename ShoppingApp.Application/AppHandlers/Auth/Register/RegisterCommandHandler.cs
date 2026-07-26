using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Core.Abstractions.Repository;
using ShoppingApp.Core.Errors;
using ShoppingApp.Core.Repository;
using ShoppingApp.Core.Utility;

namespace ShoppingApp.Application.AppHandlers.Auth.Register;

public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{
    private readonly IAuthRepository _authRepository;

    public RegisterCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<Result> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await _authRepository.GetUserByEmail(command.Email, cancellationToken);
        if (existingUser != null)
        {
            return AuthErrors.UserAlreadyExistsError(command.Email);
        }

        var identityResult = await _authRepository.RegisterUser(command.Username, command.Email, command.Password, cancellationToken);
        if (!identityResult.Succeeded)
        {
            return AuthErrors.UserCreationError(identityResult.Errors);
        }

        return Result.Success();
    }
}
