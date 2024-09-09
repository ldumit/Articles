using Articles.Abstractions;
using Articles.Entitities;

namespace Production.Domain.Entities;

public class ArticleCurrentStage : ChildEntity
{
    public int ArticleId { get; set; }
    public Article Article { get; set; } = null!;
    public ArticleStage Stage { get; set; }
    //public Stage Stage { get; set; } = null!;
}