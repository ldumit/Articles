using Articles.Abstractions;
using Mapster;
using Submission.Domain.Enums;
using Submission.Domain.Events;

namespace Submission.Domain.Entities;

public partial class Article 
{
		public static Article CreateArticle(string title, ArticleType Type, string ScopeStatement, int journalId, IArticleAction action)
    {
        var article = new Article
				{
						Title = title,
						Type = Type,
						Scope = ScopeStatement,
						JournalId = journalId,
				};
				article.Stage = ArticleStage.ArticleCreated;

				var domainEvent = article.Adapt<ArticleCreatedDomainEvent>() with { Action = action };
				article.AddDomainEvent(domainEvent);
				return article;
		}

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
