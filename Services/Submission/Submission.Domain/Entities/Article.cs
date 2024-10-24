using Articles.Abstractions;
using Articles.Entitities;
using Submission.Domain.Enums;

namespace Submission.Domain.Entities;

public partial class Article : AggregateEntity
{
		public required int JournalId { get; set; }
    public ArticleType Type { get; set; }
    public string Scope { get; set; }

		//public int ResearchTopicId { get; set; }

		public required string Title { get; set; }
    //public int RelatedArticleId { get; set; }
    public string ContributionToField { get; set; }

    //decide - do I need this complex property here?
    //public Submission ReadOnlyData { get; private set; } = default!;

    //public string JournalSection { get; set; } = default!;

		//todo create a separate entity for referenced users and use it uniformly for all kind of users(sumbission, author, typesetter etc.)
		public DateTime SubmitedOn { get; set; }
    public required virtual int SubmitedById { get; set; }
    public virtual Person SubmitedBy { get; set; } = null!;

    public ArticleStage Stage { get; set; }
//    public ArticleCurrentStage CurrentStage { get; set; } = null!;

    public Journal Journal { get; init; } = null!;

		// talk - ways to represent collections 
		private readonly List<Asset> _assets = new();
    public IReadOnlyList<Asset> Assets => _assets.AsReadOnly();

    public IEnumerable<Author> Authors => Actors.Where(aa => aa.Person is Author).Select(aa => aa.Person as Author);
		public List<ArticleActor> Actors { get; set; } = new() ;
    
    private readonly List<StageHistory> _stageHistories = new();
    public IReadOnlyList<StageHistory> StageHistories => _stageHistories.AsReadOnly();
}
