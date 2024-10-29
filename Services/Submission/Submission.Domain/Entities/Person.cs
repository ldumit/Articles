using Articles.Entitities;
using Submission.Domain.ValueObjects;

namespace Submission.Domain.Entities
{
		public class Person : AggregateEntity
		{
				public required string FirstName { get; set; }
				public required string LastName { get; set; }
				public string FullName => FirstName + " " + LastName;

				public string? Title { get; set; }
				public required EmailAddress Email { get; set; }

				public int? UserId { get; set; }

				public string PersonType { get; set; } = null!;

				public List<ArticleActor> ArticleActors { get; set; } = new();
		}
}
