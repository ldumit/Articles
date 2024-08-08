using Microsoft.AspNetCore.Identity;

namespace Auth.Domain;

public class UserRole : IdentityUserRole<int>
{
    public DateTime? BeginDate { get; set; }
    public DateTime? ExpiringDate { get; set; }
}
