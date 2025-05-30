﻿using Blocks.Entitities;

namespace ArticleHub.Domain.Entities;

public class Person : IEntity
{
    public int Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string FullName => FirstName + " " + LastName;

    public string? Title { get; init; }
    public required string Email { get; init; }

    public int? UserId { get; set; }
}
