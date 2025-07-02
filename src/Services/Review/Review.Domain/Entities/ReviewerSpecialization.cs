namespace Review.Domain.Entities;

public class ReviewerSpecialization : IChildEntity
{
		public required int JournalId { get; init; }
		public Journal Journal { get; init; } = null!;

		public required int ReviewerId { get; init; }
		public Reviewer Reviewer { get; init; } =  null!;
}
