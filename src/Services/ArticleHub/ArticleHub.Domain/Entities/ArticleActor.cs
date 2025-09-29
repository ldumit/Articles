using Articles.Abstractions.Enums;
using Blocks.Entitities;

namespace ArticleHub.Domain.Entities;

public class ArticleActor : Entity
{
    public UserRoleType Role { get; init; }
    public int ArticleId { get; init; }
    public int PersonId { get; init; }
    public Person Person { get; init; } = null!;
}
