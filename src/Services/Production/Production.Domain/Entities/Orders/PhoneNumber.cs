﻿using Blocks.Entitities;

namespace Ordering.Domain.Models;

public record PhoneNumber(int CountryCode, long Number) : ValueObject;