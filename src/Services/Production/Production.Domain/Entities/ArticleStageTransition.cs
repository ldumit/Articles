using Articles.Abstractions.Enums;
using Articles.Entitities;
using Articles.System.Cache;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public class ArticleStageTransition : IMetadataEntity, ICacheable
{
    public ArticleStage CurrentStage { get; set; }
    public ArticleActionType ActionType { get; set; }
    public ArticleStage DestinationStage { get; set; }
}
