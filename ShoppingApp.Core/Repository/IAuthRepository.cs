using Microsoft.AspNetCore.Identity;
using ShoppingApp.Core.Entities;

namespace ShoppingApp.Core.Repository;

public interface IAuthRepository
{
    Task<IEnumerable<IdentityRole>> GetAllRoles(CancellationToken cancellationToken);
    Task<IdentityUser?> GetUserByEmail(string email, CancellationToken cancellationToken);
    Task<bool> CheckUserPassword(IdentityUser user, string password, CancellationToken cancellationToken);
    Task<IEnumerable<string>> GetUserRoles(IdentityUser user);
    Task<IdentityResult> RegisterUser(string username, string email, string password, CancellationToken cancellationToken);
}
