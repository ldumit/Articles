using Articles.Security;

namespace Review.Domain.Entities
{
		public class ArticleActor : IChildEntity
		{
        public int ArticleId { get; init; }
				public virtual Article Article { get; init; } = null!;
				public required int PersonId { get; init; }
				public Person Person { get; init; } = null!;
				public UserRoleType Role { get; init; }

				public string TypeDiscriminator { get; init; } = null!; // EF discriminator to manage inheritance
		}
}
