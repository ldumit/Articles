using Articles.Entitities;
using Articles.Security;
using Submission.Domain.Enums;

namespace Submission.Domain.Entities
{
		public class AuthorActor: ArticleActor
		{
				public List<ContributionArea> ContributionAreas { get; init; } = null!;
		}


		//todo define agregators, entities, child entities and value objects
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
