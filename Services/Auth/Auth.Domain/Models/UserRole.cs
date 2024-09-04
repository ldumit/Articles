using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Models;

//talk - why we need save here the enum as int and not string comparing with the User entity from Production
public class UserRole : IdentityUserRole<int>
{
    public DateTime? BeginDate { get; set; }
    public DateTime? ExpiringDate { get; set; }
}
