using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class Stage : EnumEntity<ArticleStage>
{
    public string Description { get; set; } = null!;
}
