using Articles.Entitities;
using Production.Domain.Enums;
using Production.Domain.Events;

namespace Production.Domain.Entities;

public partial class Article : AuditedEntity
{
    public void SetStage(ArticleStage newStage, FileActionType actionType, string comment, int? userId = null, Enums.AssetType? assetType = null, DiscussionType discussionType = default)
    {
        if (newStage == CurrentStage.StageId)
            return;

        AddDomainEvent(new ArticleStageChangedDomainEvent()
        {
            ArticleId = Id,
            NewStage = newStage,
            PreviousStage = CurrentStage.StageId,
            AssetType = assetType,
            NewAction = actionType,
            UserId = userId,
            Comment = comment,
            DiscussionType = discussionType
        });
        _stageHistories.Add(new StageHistory { ArticleId = Id, StageId = newStage, StartDate = DateTime.UtcNow });
        //StageId = newStage;
    }
}
