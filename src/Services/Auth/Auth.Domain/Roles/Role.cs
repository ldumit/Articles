﻿using Microsoft.AspNetCore.Identity;
using Articles.Abstractions.Enums;

namespace Auth.Domain.Roles;

public class Role : IdentityRole<int>, IEntity
{
    public required UserRoleType Type { get; set; }
    public required string Description { get; set; }
}
