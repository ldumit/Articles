using Articles.Abstractions.Enums;
using Blocks.Entitities;
using Blocks.Core.Cache;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public class ArticleStageTransition : IMetadataEntity, ICacheable
{
    public ArticleStage CurrentStage { get; set; }
    public ArticleActionType ActionType { get; set; }
    public ArticleStage DestinationStage { get; set; }
}
