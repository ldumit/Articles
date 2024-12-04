namespace Review.Domain.Entities;

public partial class Author : Person
{
		public required string Affiliation { get; init; }
}
