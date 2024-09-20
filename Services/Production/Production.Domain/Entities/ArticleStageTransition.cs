using Articles.Abstractions;
using Articles.Entitities;
using Articles.System.Cache;
using Microsoft.Extensions.Caching.Memory;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public class ArticleStageTransition : IDomainMetadata, ICacheable
{
    public ArticleStage CurrentStage { get; set; }
    public ArticleActionType ActionType { get; set; }
    public ArticleStage DestinationStage { get; set; }
}
