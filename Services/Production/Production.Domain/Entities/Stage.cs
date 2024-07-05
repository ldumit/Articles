using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class Stage : EnumEntity2<ArticleStagesCode>
{
    public string Description { get; set; }
}
