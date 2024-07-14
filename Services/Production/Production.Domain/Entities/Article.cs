using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class Article : TenantEntity
{
    public int ArticleId { get; set; }    
    public string? Title { get; set; }    
    public string Type { get; set; } = null!;
    public string Doi { get; set; } = null!;
    
    //todo create a separate entity for referenced users and use it uniformly for all kind of users(sumbission, author, typesetter etc.)
    public DateTime? SubmissionDate { get; set; }
    public virtual User SubmissionUser { get; set; }

    public ArticleStagesCode StageId { get; private set; }
    public Stage Stage { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public int JournalId { get; set; }

    public DateTime? PublishedOn { get; set; }

    public DateTime? AcceptedOn { get; set; }
    public int FieldId { get; set; }
    public string FieldName { get; set; }
    public DateTime? RecommendedPublicationDate { get; set; }
    public int VolumeId { get; set; }
    public DateTime? JournalTransferCompletedDate { get; set; }

    public virtual int TypesetterId { get; set; }
    public virtual Typesetter? Typesetter { get; set; }

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Journal? Journal { get; set; }

    public virtual ICollection<StageHistory> StageHistories { get; set; } = new List<StageHistory>();

    //public virtual ICollection<ArticleReviewEditor> ArticleReviewEditor { get; set; } = new List<ArticleReviewEditor>();
    //public virtual ArticleAssociateEditor? ArticleAssociateEditor { get; set; }
}
