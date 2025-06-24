namespace Review.Domain.Entities;

public partial class Reviewer : Person
{
		//todo make this private and expose the readonly list
		public List<Journal> Specializations { get; init; } = null!;
}
