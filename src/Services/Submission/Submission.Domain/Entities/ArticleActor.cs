using Articles.Entitities;
using Articles.Security;

namespace Submission.Domain.Entities
{
		//todo rename Actor to Contributor?!?
		public class ArticleActor : IChildEntity
		{
        public int ArticleId { get; set; }
				public virtual Article Article { get; set; } = null!;
				public required int PersonId { get; set; }
				public Person Person { get; set; } = null!;
				public UserRoleType Role { get; set; }

				public string ActorType { get; set; } = null!; // EF discriminator
		}
}
