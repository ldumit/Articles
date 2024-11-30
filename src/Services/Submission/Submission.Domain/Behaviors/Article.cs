using Mapster;
using Blocks.Domain;
using Articles.Security;
using Submission.Domain.StateMachines;

namespace Submission.Domain.Entities;

public partial class Article
{
		public void SetStage(ArticleStage newStage, IArticleAction<ArticleActionType> action, ArticleStateMachineFactory stateMachineFactory)
    {
				if (newStage == Stage)
            return;

				stateMachineFactory.ValidateStageTransition(Stage, action.ActionType);

				var currentStage = Stage;
				Stage = newStage;
        LasModifiedOn = action.CreatedOn;
				LastModifiedById = action.CreatedById;

				_stageHistories.Add(
						new StageHistory { ArticleId = Id, StageId = newStage, StartDate = DateTime.UtcNow });
				
				AddDomainEvent(
						new ArticleStageChangedDomainEvent(action, currentStage, newStage));
    }

		public void AssignAuthor(Author author, HashSet<ContributionArea> contributionAreas, bool isCorrespondingAuthor, IArticleAction<ArticleActionType> action)
		{
				var role = isCorrespondingAuthor ? UserRoleType.CORAUT : UserRoleType.AUT;				
				
				if (Contributors.Exists(a => a.PersonId == author.Id && a.Role == role))
						throw new DomainException($"Author {author.Email} is already assigned to the article");

				Contributors.Add(new ArticleAuthor() {
						ContributionAreas = contributionAreas,
						PersonId = author.Id, 
						Role = role
				});
				AddDomainEvent(
						new AuthorAssigned(action, author.Id, author.UserId!.Value));
				AddAction(action);
		}

		public void Approve(IArticleAction<ArticleActionType> action, ArticleStateMachineFactory _stateMachineFactory)
		{
				SetStage(ArticleStage.InitialApproved, action, _stateMachineFactory);
				
				AddDomainEvent(new ArticleApprovedDomainEvent(this, action));
		}

		public void Submit(IArticleAction<ArticleActionType> action, ArticleStateMachineFactory _stateMachineFactory)
		{
				SubmittedById = action.CreatedById;
				SubmittedOn = action.CreatedOn;

				SetStage(ArticleStage.Submitted, action, _stateMachineFactory);
		}

		public Asset CreateAsset(AssetTypeDefinition type, IArticleAction<ArticleActionType> action)
    {
				byte? maxAssetNumber = _assets
						.Where(a => a.Type == type.Id)
						.Select(a => (byte?)a.Number) // Cast to byte? to allow nulls
						.Max();

				byte nextAssetNumber;
				if (maxAssetNumber is not null)
						nextAssetNumber = (byte)(maxAssetNumber + 1);
				else
						nextAssetNumber = type.AllowsMultipleAssets ? (byte)1 : (byte)0; // for asset types that allow multiple assets of the same type, start from 1, otherwise 0

				var asset = Asset.Create(this, type, nextAssetNumber);
        _assets.Add(asset);
				
				AddAction(action);
				return asset;
    }

		private void AddAction(IArticleAction<ArticleActionType> action)
		{
				_actions.Add(action.Adapt<ArticleAction>());
				AddDomainEvent(new ArticleActionExecuted(action, this));
		}
}
