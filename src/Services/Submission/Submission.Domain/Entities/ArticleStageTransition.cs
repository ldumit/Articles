using Articles.Abstractions.Enums;
using Articles.Entitities;
using Articles.System.Cache;
using Submission.Domain.Enums;

namespace Submission.Domain.Entities;

public class ArticleStageTransition : IMetadataEntity, ICacheable
{
    public ArticleStage CurrentStage { get; set; }
    public ArticleActionType ActionType { get; set; }
    public ArticleStage DestinationStage { get; set; }
}
