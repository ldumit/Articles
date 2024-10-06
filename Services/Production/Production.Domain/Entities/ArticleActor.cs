using Articles.Entitities;
using Articles.Security;

namespace Production.Domain.Entities
{
		public class ArticleActor : ChildEntity
		{
        public int ArticleId { get; set; }
				public virtual Article Article { get; set; } = null!;
				public required int PersonId { get; set; }
				public Person Person { get; set; } = null!;
				public UserRoleType Role { get; set; }
    }
}
