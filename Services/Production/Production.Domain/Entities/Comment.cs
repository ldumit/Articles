using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class Comment:TenantEntity
{

    public int ArticleId { get; set; }

    public CommentTypeTopic TypeId { get; set; }

    public string Text { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime? CommentModificationDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModificationDate { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual CommentType Type { get; set; } = null!;
}
