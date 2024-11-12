using Articles.Entitities;

namespace Production.Domain.Entities
{
		public class Person : Entity
		{
				public required string FirstName { get; init; }
				public required string LastName { get; init; }
				public string FullName => FirstName + " " + LastName;

				public string? Title { get; init; }
				public required string Email { get; init; }

				public int? UserId { get; set; }

				public List<ArticleContributor> ArticleContributors { get; set; } = new();

				public string TypeDiscriminator { get; init; } = null!; // EF discriminator
		}
}
