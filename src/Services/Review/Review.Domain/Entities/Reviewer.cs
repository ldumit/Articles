namespace Review.Domain.Entities;

public class Reviewer : Person
{
		public required string Affiliation { get; init; }

		//todo make this private and expose the readonly list
		public List<Journal> Specializations { get; init; } = null!;
}
