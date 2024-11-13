using Articles.Abstractions.Enums;
using Blocks.Entitities;

namespace Production.Domain.Entities;

public partial class Stage : EnumEntity<ArticleStage>
{
    public string Info { get; set; } = null!;
}
