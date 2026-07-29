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
    public RefreshTokenRepository(ShoppingAppDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<RefreshToken?> GetByToken(string token, CancellationToken cancellationToken)
    {
        return await _dbContext.RefreshTokens
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Token == token, cancellationToken);
    }
}
