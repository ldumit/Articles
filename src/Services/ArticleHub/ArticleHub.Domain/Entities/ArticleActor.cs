using Articles.Abstractions.Enums;

namespace ArticleHub.Domain.Entities;

public class ArticleActor
{
    public UserRoleType Role { get; init; }
    public int ArticleId { get; init; }
    public int PersonId { get; init; }
    public Person Person { get; init; } = null!;
}
