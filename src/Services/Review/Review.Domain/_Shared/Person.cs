using Review.Domain.Shared.ValueObjects;

namespace Review.Domain.Shared;

public class Person : AggregateRoot
{
		public required string FirstName { get; init; }
		public required string LastName { get; init; }
		public string FullName => FirstName + " " + LastName;

		public string? Honorific { get; init; }
		public required EmailAddress Email { get; init; }
		public required string Affiliation { get; init; }

		public int? UserId { get; init; }

		public virtual string TypeDiscriminator { get; init; } = null!; // EF discriminator
}
