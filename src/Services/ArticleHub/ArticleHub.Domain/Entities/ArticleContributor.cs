using Articles.Security;

namespace ArticleHub.Domain.Entities;

public class ArticleContributor
{
    public UserRoleType Role { get; init; }
    public int ArticleId { get; init; }
    public required int PersonId { get; init; }
    public Person Person { get; init; } = null!;
}
