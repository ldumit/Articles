using Articles.Entitities;

namespace Production.Domain.Entities
{
		public class Person : Entity
		{
				public required string FirstName { get; set; }
				public required string LastName { get; set; }
				public string? Title { get; set; }
				public required string Email { get; set; }

				public int? UserId { get; set; }
				//public User? User { get; set; }

				public string PersonType { get; set; } = null!;

        public required List<ArticleActor> ArticleActors { get; set; }
		}
}
