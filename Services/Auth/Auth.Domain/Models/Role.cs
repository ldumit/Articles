using Articles.Entitities;
using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Models;

public class Role : IdentityRole<int>, IEntity<int>
{
    public required UserRoleType Type { get; set; }
    public required string Description { get; set; }
}
