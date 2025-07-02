namespace Review.Domain.Entities;

public partial class Journal : Entity
{
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }

		
		private readonly List<Article> _articles = new();
		public IReadOnlyList<Article> Articles => _articles.AsReadOnly();


		public IReadOnlyCollection<ReviewerSpecialization> Reviewers { get; set; } = new HashSet<ReviewerSpecialization>();

		//public int ChiefEditorId { get; private set; }
		//public Reviewer ChiefEditor { get; private set; } = null!;


		//private readonly List<JournalEditor> _editors = new();
		//public IReadOnlyList<JournalEditor> Editors => _editors.AsReadOnly();
}
