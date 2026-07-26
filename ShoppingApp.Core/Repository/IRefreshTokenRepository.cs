using ShoppingApp.Core.Abstractions.Repository;
using ShoppingApp.Core.Entities;

namespace ShoppingApp.Core.Repository;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task SaveRefreshToken(string userId, string token, CancellationToken cancellationToken);
    Task<RefreshToken?> GetByToken(string token, CancellationToken cancellationToken);
}
