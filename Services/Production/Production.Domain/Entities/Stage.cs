using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class Stage : EnumEntity<ArticleStagesCode>
{
    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }
    public string Description { get; set; }
    public virtual StageCode CodeNavigation { get; set; } = null!;
}
