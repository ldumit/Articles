using Blocks.Security;

namespace ArticleHub.Domain;

public class ArticleContributor
{
		public int Id { get; set; }

    public string Affiliation { get; init; } = null!;

		public UserRoleType Role { get; init; }

		public int ArticleId { get; init; }

		public required int PersonId { get; init; }
		public Person Person { get; init; } = null!;
}
