using Articles.Abstractions;
using Articles.Entitities;
using Articles.Security;
using Production.Domain.Events;

namespace Production.Domain.Entities;

public partial class Article : AuditedEntity
{
    public void SetStage(ArticleStage newStage, IArticleAction action)
    {
        if (newStage == CurrentStage.Stage)
            return;

        //todo chose between the next 2 aproaches
        CurrentStage.Stage = newStage;
				Stage = newStage;

				AddDomainEvent(
            new ArticleStageChangedDomainEvent(action, newStage, CurrentStage.Stage)
            );
        _stageHistories.Add(new StageHistory { ArticleId = Id, StageId = newStage, StartDate = DateTime.UtcNow });
    }

    public void SetTypesetter(Typesetter typesetter, IArticleAction action)
    {
        if(this.Typesetter is not null)
				    throw new TypesetterAlreadyAssignedException("Typesetter aldready assigned");
        
        Actors.Add(new ArticleActor() { PersonId = typesetter.Id, Role = UserRoleType.TSOF});

        AddDomainEvent(new TypesetterAssignedDomainEvent(action, typesetter.Id, typesetter.UserId.Value));
    }
}
