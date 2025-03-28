using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Blocks.Domain;
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
            new ArticleStageChanged(action, currentStage, newStage)
            );
    }

    public void AssignTypesetter(Typesetter typesetter, IArticleAction<ArticleActionType> action)
    {
        if(this.Typesetter is not null)
				    throw new TypesetterAlreadyAssignedException("Typesetter aldready assigned");

				_contributors.Add(new ArticleContributor() { PersonId = typesetter.Id, Role = UserRoleType.TSOF});

        AddDomainEvent(new TypesetterAssigned(action, typesetter.Id, typesetter.UserId!.Value));
    }

		public Asset CreateAsset(AssetTypeDefinition type, byte assetNumber = 0)
    {
        if (_assets.Exists(a => a.Type == type.Id && a.Number == assetNumber))
            throw new DomainException("Asset already exists");

				var asset = Asset.Create(this, type, assetNumber);
        _assets.Add(asset);        
        return asset;
    }
}
