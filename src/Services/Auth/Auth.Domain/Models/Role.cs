﻿using Blocks.Entitities;
using Articles.Security;
using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Models;

public class Role : IdentityRole<int>, IEntity
{
    public required UserRoleType Type { get; set; }
    public required string Description { get; set; }
}
