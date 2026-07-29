using Microsoft.Extensions.Options;
using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.AppServices.Implementation.Auth;
using ShoppingApp.Application.Dto;
using ShoppingApp.Application.Settings;
using ShoppingApp.Core.Abstractions.Repository;
using ShoppingApp.Core.Entities;
using ShoppingApp.Core.Errors;
using ShoppingApp.Core.Repository;
using ShoppingApp.Core.Utility;

namespace ShoppingApp.Application.AppHandlers.Auth.Login;

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponseDto>
{
    private readonly IAuthRepository _authRepository;
    private readonly IAuthService _authService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtSettings _jwtSettings;

    public LoginCommandHandler(IAuthRepository authRepository,
        IAuthService authService,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork,
        IOptions<JwtSettings> jwtOptions)
    {
        _authRepository = authRepository;
        _authService = authService;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
        _jwtSettings = jwtOptions.Value;
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

        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryInDays),
            Valid = true
        };

        _refreshTokenRepository.Add(refreshTokenEntity);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success(new LoginResponseDto(accessToken, refreshToken));
    }
}
