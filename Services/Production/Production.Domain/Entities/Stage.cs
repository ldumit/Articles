using Articles.Abstractions;
using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class Stage : EnumEntity<ArticleStage>
{
    public string Info { get; set; } = null!;
}
