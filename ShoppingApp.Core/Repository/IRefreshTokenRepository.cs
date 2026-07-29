using ShoppingApp.Core.Abstractions.Repository;
using ShoppingApp.Core.Entities;

namespace ShoppingApp.Core.Repository;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetByToken(string token, CancellationToken cancellationToken);
}
