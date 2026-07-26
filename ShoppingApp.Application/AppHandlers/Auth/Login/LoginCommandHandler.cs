using Microsoft.Extensions.Options;
using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.AppServices.Implementation.Auth;
using ShoppingApp.Application.Dto;
using ShoppingApp.Application.Settings;
using ShoppingApp.Core.Errors;
using ShoppingApp.Core.Repository;
using ShoppingApp.Core.Utility;

namespace ShoppingApp.Application.AppHandlers.Auth.Login;

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponseDto>
{
    private readonly IAuthRepository _authRepository;
    private readonly IAuthService _authService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LoginCommandHandler(IAuthRepository authRepository,
        IAuthService authService,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _authRepository = authRepository;
        _authService = authService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Result<LoginResponseDto>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _authRepository.GetUserByEmail(command.email, cancellationToken);
        if (user == null)
        {
            return Result.Failure<LoginResponseDto>(AuthErrors.UserNotFound());
        }

        var success = await _authRepository.CheckUserPassword(user, command.password, cancellationToken);
        if (!success)
        {
            return Result.Failure<LoginResponseDto>(AuthErrors.InvalidCredentials());
        }

        var accessToken = await _authService.GenerateAccessToken(user);
        var refreshToken = _authService.GenerateRefreshToken();

        await _refreshTokenRepository.SaveRefreshToken(user.Id, refreshToken, cancellationToken);

        return Result.Success(new LoginResponseDto(accessToken, refreshToken));
    }
}
