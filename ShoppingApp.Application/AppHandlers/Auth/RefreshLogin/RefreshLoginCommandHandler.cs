using ShoppingApp.Application.Abstractions.Messaging;
using ShoppingApp.Application.AppServices.Implementation.Auth;
using ShoppingApp.Application.Dto;
using ShoppingApp.Core.Abstractions.Repository;
using ShoppingApp.Core.Errors;
using ShoppingApp.Core.Repository;
using ShoppingApp.Core.Utility;

namespace ShoppingApp.Application.AppHandlers.Auth.RefreshLogin;

public class RefreshLoginCommandHandler : ICommandHandler<RefreshLoginCommand, LoginResponseDto>
{
    private readonly IAuthRepository _authRepository;
    private readonly IAuthService _authService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshLoginCommandHandler(IAuthRepository authRepository,
        IAuthService authService,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork)
    {
        _authRepository = authRepository;
        _authService = authService;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<LoginResponseDto>> Handle(RefreshLoginCommand command, CancellationToken cancellationToken)
    {
        var refreshTokenEntity = await _refreshTokenRepository.GetByToken(command.refreshToken, cancellationToken);
        if (refreshTokenEntity == null || !refreshTokenEntity.Valid || refreshTokenEntity.ExpiresAt <= DateTime.UtcNow)
        {
            return Result.Failure<LoginResponseDto>(AuthErrors.InvalidCredentials());
        }

        var newAccessToken = await _authService.GenerateAccessToken(refreshTokenEntity.User!);
        var newRefreshToken = _authService.GenerateRefreshToken();

        refreshTokenEntity.Token = newRefreshToken;
        _refreshTokenRepository.Update(refreshTokenEntity);
        await _unitOfWork.Commit(cancellationToken);


        return Result.Success(new LoginResponseDto(newAccessToken,
            newRefreshToken
        ));
    }
}