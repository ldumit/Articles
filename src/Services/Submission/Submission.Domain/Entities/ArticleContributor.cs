using Articles.Security;

namespace Submission.Domain.Entities
{
		public class ArticleContributor : IChildEntity
		{
        public int ArticleId { get; init; }
				public virtual Article Article { get; init; } = null!;
				public int PersonId { get; init; }
				public required Person Person { get; init; } = null!;
				public UserRoleType Role { get; init; }

				public string TypeDiscriminator { get; init; } = null!; // EF discriminator to manage inheritance
		}
}
