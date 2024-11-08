using Articles.Abstractions;
using Articles.Entitities;
using Submission.Domain.Enums;

namespace Submission.Domain.Entities;

public partial class Article : AggregateEntity
{
		public required string Title { get; init; }
		public ArticleType Type { get; init; }
		public string Scope { get; init; } = default!;

		//todo: create a complex object for the submission, the following 3 properties ??!?!?!?!
		public DateTime? SubmittedOn { get; set; }
    public virtual int? SubmittedById { get; set; }
    public virtual Person? SubmittedBy { get; set; }

    public ArticleStage Stage { get; set; }

		public required int JournalId { get; init; }
		public Journal Journal { get; init; } = null!;
		//public string JournalSection { get; set; } = default!;

		// talk - ways to represent collections 
		private readonly List<Asset> _assets = new();
    public IReadOnlyList<Asset> Assets => _assets.AsReadOnly();

    //public IEnumerable<Author> Authors => Actors.Where(aa => aa.Person is Author).Select(aa => aa.Person as Author);

		// todo: rename to Contributors?!
		public List<ArticleActor> Actors { get; set; } = new() ;
    
    private readonly List<StageHistory> _stageHistories = new();
    public IReadOnlyList<StageHistory> StageHistories => _stageHistories.AsReadOnly();
}
