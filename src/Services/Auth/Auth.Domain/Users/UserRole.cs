using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Users;

//talk - why we need save here the enum as int and not string comparing with the User entity from Production
public partial class UserRole : IdentityUserRole<int>
{
    public DateTime? StartDate { get; set; }
    public DateTime? ExpiringDate { get; set; }
}
