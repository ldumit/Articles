using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class Article : AuditedEntity
{
    //decide - do I need this complex property here?
    //public Submission ReadOnlyData { get; private set; } = default!;

    public required string Title { get; set; }
    //public required string Type { get; set; }
    public required string Doi { get; set; }
    
    //todo create a separate entity for referenced users and use it uniformly for all kind of users(sumbission, author, typesetter etc.)
    public DateTime SubmitedOn { get; set; }
    public required virtual int SubmitedById { get; set; }
    public required virtual User SubmitedBy { get; set; }

    public int CurrentStageId { get; set; }
    public ArticleCurrentStage CurrentStage { get; set; } = null!;
    //public ArticleStage Stage => CurrentStage.StageId;

    public required int JournalId { get; set; }
    public int VolumeId { get; set; }

    public DateTime? PublishedOn { get; set; }
    public User? PublishedBy{ get; set; }

    public DateTime AcceptedOn { get; set; }

    
    public virtual int? TypesetterId { get; set; }
    public virtual Typesetter? Typesetter { get; set; }
    public Journal Journal { get; } = null!;

    // talk - ways to represent collections 
    public List<Asset> Assets { get; }  = new() ;

    public IEnumerable<Author> Authors { get; } = new List<Author>();

    public ICollection<Comment> Comments { get; } = new List<Comment>();
    
    private readonly List<StageHistory> _stageHistories = new();
    public IReadOnlyList<StageHistory> StageHistories => _stageHistories.AsReadOnly();
}
