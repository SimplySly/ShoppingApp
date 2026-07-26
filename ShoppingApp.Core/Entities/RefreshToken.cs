using Microsoft.AspNetCore.Identity;

namespace ShoppingApp.Core.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool Valid { get; set; }

    public IdentityUser User { get; set; }
}
