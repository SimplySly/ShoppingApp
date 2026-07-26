using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingApp.Application.Settings;
using ShoppingApp.Core.Abstractions.Repository;
using ShoppingApp.Core.Entities;
using ShoppingApp.Core.Repository;
using ShoppingApp.Infrastructure.Database;

namespace ShoppingApp.Infrastructure.Repository.AppServices;

public sealed class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
{
    private readonly JwtSettings _jwtSettings;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenRepository(ShoppingAppDbContext dbContext,
        IOptions<JwtSettings> jwtOptions,
        IUnitOfWork unitOfWork)
        : base(dbContext)
    {
        _jwtSettings = jwtOptions.Value;
        _unitOfWork = unitOfWork;
    }

    public async Task<RefreshToken?> GetByToken(string token, CancellationToken cancellationToken)
    {
        return await _dbContext.RefreshTokens
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Token == token, cancellationToken);
    }

    public async Task SaveRefreshToken(string userId, string token, CancellationToken cancellationToken)
    {
        var refreshToken = new RefreshToken
        {
            Token = token,
            UserId = userId,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryInDays),
            Valid = true
        };

        Add(refreshToken);

        await _unitOfWork.Commit(cancellationToken);
    }
}
