using Articles.Abstractions;
using Articles.Security;
using Submission.Domain.Enums;
using Submission.Domain.Events;

namespace Submission.Domain.Entities;

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

		public Asset CreateAsset(AssetType type, byte assetNumber = 0)
    {
        var asset = Asset.Create(this, type, assetNumber);
        _assets.Add(asset);        
        return asset;
    }
}
