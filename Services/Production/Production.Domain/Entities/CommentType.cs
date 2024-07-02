using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class CommentType: EnumEntity<CommentTypeTopic>
{
    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public virtual CommentTypeCode CodeNavigation { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();
}
