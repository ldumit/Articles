﻿using Auth.Domain.Users.Enums;

namespace Auth.Domain.Users.Events;

public record UserCreated(User User, string RessetPasswordToken);
