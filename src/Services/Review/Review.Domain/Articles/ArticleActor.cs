using Review.Domain.Articles;

namespace Review.Domain.Articles;

public class ArticleActor : IChildEntity
{
    public int ArticleId { get; init; }
		public Article Article { get; init; } = null!;
		public int PersonId { get; init; }
		public Person Person { get; init; } = null!;
		public UserRoleType Role { get; init; }

		public string TypeDiscriminator { get; init; } = null!; // EF discriminator to manage inheritance
}
