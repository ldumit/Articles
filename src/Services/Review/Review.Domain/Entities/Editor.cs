namespace Review.Domain.Entities;

public class Editor : Reviewer
{
		private readonly List<JournalEditor> _journals = new();
		public IReadOnlyList<JournalEditor> Journals => _journals.AsReadOnly();
}
