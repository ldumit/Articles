using Articles.Entitities;
using Articles.Security;
using Production.Domain.Enums;
using Production.Domain.Events;

namespace Production.Domain.Entities;

public partial class Article : AuditedEntity
{
    public void SetStage(ArticleStage newStage, ActionType actionType, string comment, int? userId = null, Enums.AssetType? assetType = null)
    {
        if (newStage == CurrentStage.Stage)
            return;

        //todo chose between the next 2 aproaches
        CurrentStage.Stage = newStage;
				Stage = newStage;

				AddDomainEvent(new ArticleStageChangedDomainEvent()
        {
            ArticleId = Id,
            NewStage = newStage,
            PreviousStage = CurrentStage.Stage,
            AssetType = assetType,
            NewAction = actionType,
            UserId = userId,
            Comment = comment,
            //DiscussionType = discussionType
        });
        _stageHistories.Add(new StageHistory { ArticleId = Id, StageId = newStage, StartDate = DateTime.UtcNow });
    }

    public void SetTypesetter(Typesetter typesetter)
    {
				//if (_actors.Any(u => u.Role == UserRoleType.TSOF))
        if(this.Typesetter is not null)
				    throw new TypesetterAlreadyAssignedException("Typesetter aldready assigned");
        
        Actors.Add(new ArticleActor() { PersonId = typesetter.Id, Role = UserRoleType.TSOF});
    }
}
