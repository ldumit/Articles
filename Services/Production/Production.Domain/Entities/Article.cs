using Articles.Abstractions;
using Articles.Entitities;
using Newtonsoft.Json;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class Article : AuditedEntity
{
    //decide - do I need this complex property here?
    //public Submission ReadOnlyData { get; private set; } = default!;

    public required string Title { get; set; }
    public required string Doi { get; set; }
    
    //todo create a separate entity for referenced users and use it uniformly for all kind of users(sumbission, author, typesetter etc.)
    public DateTime SubmitedOn { get; set; }
    public required virtual int SubmitedById { get; set; }
    public virtual Person SubmitedBy { get; set; } = null!;

    public ArticleStage Stage { get; set; }
    public ArticleCurrentStage CurrentStage { get; set; } = null!;
    //public ArticleStage Stage => CurrentStage.StageId;

    public required int JournalId { get; set; }
    public int VolumeId { get; set; }

    public DateTime? PublishedOn { get; set; }
    public Person? PublishedBy{ get; set; }

    public DateTime AcceptedOn { get; set; }

    public Journal Journal { get; } = null!;

    // talk - ways to represent collections 
    public List<Asset> Assets { get; }  = new() ;

    public IEnumerable<Author> Authors => Actors.Where(aa => aa.Person is Author).Select(aa => aa.Person as Author);
		public Typesetter? Typesetter => Actors.Where(aa => aa.Person is Typesetter).Select(aa => aa.Person as Typesetter).FirstOrDefault();

    //talk about private field and json deserielizer
		//[JsonProperty("Actors")]
		//private readonly List<ArticleActor> _actors = new();
    //public IReadOnlyCollection<ArticleActor> Actors => _actors.AsReadOnly();
		public List<ArticleActor> Actors { get; set; } = new() ;

		public ICollection<Comment> Comments { get; } = new List<Comment>();
    
    private readonly List<StageHistory> _stageHistories = new();
    public IReadOnlyList<StageHistory> StageHistories => _stageHistories.AsReadOnly();
}
