namespace Submission.Domain.Entities
{
		public class Person : AggregateEntity
		{
				public required string FirstName { get; init; }
				public required string LastName { get; init; }
				public string FullName => FirstName + " " + LastName;

				public string? Title { get; init; }
				public required EmailAddress Email { get; init; }

				public int? UserId { get; init; }

				public IReadOnlyList<ArticleContributor> ArticleContributors { get; private set; } = new List<ArticleContributor>();

				public string TypeDiscriminator { get; init; } = null!; // EF discriminator
		}
}
