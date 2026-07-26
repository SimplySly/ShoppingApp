using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Repository;
using ShoppingApp.Core.Static;
using ShoppingApp.Infrastructure.Database;

namespace ShoppingApp.Infrastructure.Repository.AppServices;

public sealed class AuthRepository : IAuthRepository
{
    private readonly ShoppingAppDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthRepository(ShoppingAppDbContext dbContext,
        UserManager<IdentityUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<IEnumerable<IdentityRole>> GetAllRoles(CancellationToken cancellationToken)
    {
        return await _dbContext.Roles
            .AsNoTracking()
            .ToListAsync(cancellationToken  );
    }

    public async Task<IdentityUser?> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<IEnumerable<string>> GetUserRoles(IdentityUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<bool> CheckUserPassword(IdentityUser user, string password, CancellationToken cancellationToken)
    {
        var success = await _userManager.CheckPasswordAsync(user, password);

        return success;
    }

    public async Task<IdentityResult> RegisterUser(string username, string email, string password, CancellationToken cancellationToken)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        var user = new IdentityUser
        {
            UserName = username,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        { 
            return result;
        }

        result = await _userManager.AddToRoleAsync(user, AuthRoles.Customer);
        if (!result.Succeeded)
        {
            return result;
        }

        await transaction.CommitAsync(cancellationToken);

        return result;
    }
}
