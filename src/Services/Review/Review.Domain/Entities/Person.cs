namespace Review.Domain.Entities
{
		public class Person : AggregateEntity
		{
				public required string FirstName { get; init; }
				public required string LastName { get; init; }
				public string FullName => FirstName + " " + LastName;

				public string? Honorific { get; init; }
				public required EmailAddress Email { get; init; }
				public required string Affiliation { get; init; }

				public int? UserId { get; init; }

				public IReadOnlyList<ArticleActor> ArticleActors { get; private set; } = new List<ArticleActor>();

				public virtual string TypeDiscriminator { get; init; } = null!; // EF discriminator
		}
}
