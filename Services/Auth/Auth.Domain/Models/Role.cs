using Articles.Entitities;
using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Models;

public class Role : IdentityRole<int>, IEntity<int>
{
    public UserRoleType Code { get; set; }
}
