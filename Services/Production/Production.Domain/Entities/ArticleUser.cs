using Articles.Entitities;
using Articles.Security;

namespace Production.Domain.Entities
{
		public class ArticleUser : Entity
		{
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public UserRoleType Role { get; set; }
    }
}
