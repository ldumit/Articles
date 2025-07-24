namespace Review.Domain.Entities;

public partial class Reviewer : Person
{
		private readonly HashSet<ReviewerSpecialization> _specializations = new();
		public IReadOnlyCollection<ReviewerSpecialization> Specializations => _specializations;
}
