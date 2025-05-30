﻿namespace Ordering.Domain.Models;
public record Contact
{
    public required Address Address { get; init; }
    public required PhoneNumber MobilePhone { get; init; }
    public required PhoneNumber WorkPhone { get; init; }
}
