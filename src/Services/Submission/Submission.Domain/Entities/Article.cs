namespace Submission.Domain.Entities;

public partial class Article : AggregateEntity
{
		public required string Title { get; init; }
		public ArticleType Type { get; init; }
		public string Scope { get; init; } = default!;

		public DateTime? SubmittedOn { get; set; }
    public int? SubmittedById { get; set; }
    public Person? SubmittedBy { get; set; }

    public ArticleStage Stage { get; set; }

		public required int JournalId { get; init; }
		public Journal Journal { get; init; } = null!;
		//public string JournalSection { get; set; } = default!;

		// talk - ways to represent collections 
		private readonly List<Asset> _assets = new();
    public IReadOnlyList<Asset> Assets => _assets.AsReadOnly();

		public List<ArticleContributor> Contributors { get; set; } = new() ;
		//public IEnumerable<Author> Authors => Contributors.Where(aa => aa.Person is Author).Select(aa => aa.Person as Author);

		private readonly List<StageHistory> _stageHistories = new();
    public IReadOnlyList<StageHistory> StageHistories => _stageHistories.AsReadOnly();

		private readonly List<ArticleAction> _actions = new();
		public IReadOnlyList<ArticleAction> Actions => _actions.AsReadOnly();
}
