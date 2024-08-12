using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Models;

public class UserRole : IdentityUserRole<int>
{
    public DateTime? BeginDate { get; set; }
    public DateTime? ExpiringDate { get; set; }
}
