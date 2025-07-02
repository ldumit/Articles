namespace Review.Domain.Entities;

public class Editor : Reviewer
{
		private readonly HashSet<JournalEditor> _journals = new();
		public IReadOnlyCollection<JournalEditor> JournalEditors => _journals;
}
