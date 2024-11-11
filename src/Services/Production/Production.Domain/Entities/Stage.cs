using Articles.Abstractions.Enums;
using Articles.Entitities;

namespace Production.Domain.Entities;

public partial class Stage : EnumEntity<ArticleStage>
{
    public string Info { get; set; } = null!;
}
