using Articles.Abstractions;
using Articles.Security;
using Production.Domain.Enums;
using Production.Domain.Events;

namespace Production.Domain.Entities;

public partial class Article 
{
    public void SetStage(ArticleStage newStage, IArticleAction action)
    {
        if (newStage == Stage)
            return;

        var currentStage = Stage;
				Stage = newStage;
        LasModifiedOn = action.CreatedOn;
				LastModifiedById = action.CreatedById;

				_stageHistories.Add(new StageHistory { ArticleId = Id, StageId = newStage, StartDate = DateTime.UtcNow });
				AddDomainEvent(
            new ArticleStageChangedDomainEvent(action, currentStage, newStage)
            );
    }

    public void SetTypesetter(Typesetter typesetter, IArticleAction<ArticleActionType> action)
    {
        if(this.Typesetter is not null)
				    throw new TypesetterAlreadyAssignedException("Typesetter aldready assigned");
        
        Actors.Add(new ArticleActor() { PersonId = typesetter.Id, Role = UserRoleType.TSOF});

        AddDomainEvent(new TypesetterAssignedDomainEvent(action, typesetter.Id, typesetter.UserId!.Value));
    }

		public Asset CreateAsset(AssetType type, byte assetNumber = 0)
    {
        var asset = Asset.Create(this, type, assetNumber);
        _assets.Add(asset);        
        return asset;
    }
}
