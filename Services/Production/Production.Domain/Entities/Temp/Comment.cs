using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities.Temp;

public partial class Comment : AuditedEntity
{
    public int ArticleId { get; set; }

    //decide
    public CommentType TypeId { get; set; }

    public string Text { get; set; } = null!;

    public virtual Article Article { get; set; } = null!;
}
