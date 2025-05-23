﻿using Blocks.Entitities;

namespace ArticleHub.Domain.Entities;

public class Journal : IEntity
{
    public int Id { get; init; }
    public required string Abbreviation { get; init; }
    public required string Name { get; init; }

    public virtual ICollection<Article> Articles { get; } = new List<Article>();
}
