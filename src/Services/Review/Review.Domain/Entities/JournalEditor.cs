namespace Review.Domain.Entities;

public class JournalEditor : IChildEntity
{
		public required int JournalId { get; init; }
		public Journal Journal { get; init; } = null!;

		public required int EditorId { get; init; }
		public Editor Editor { get; init; } =  null!;
}
