using Mapster;
using Blocks.Domain;
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
						new ArticleStageChanged(currentStage, newStage, action));
    }

		public void AssignAuthor(Author author, HashSet<ContributionArea> contributionAreas, bool isCorrespondingAuthor, IArticleAction<ArticleActionType> action)
		{
				var role = isCorrespondingAuthor ? UserRoleType.CORAUT : UserRoleType.AUT;				
				
				if (Actors.Exists(a => a.PersonId == author.Id && a.Role == role))
						throw new DomainException($"Author {author.Email} is already assigned to the article");

				Actors.Add(new ArticleAuthor() {
						ContributionAreas = contributionAreas,
						Person = author,
						//PersonId = author.Id, 
						Role = role
				});
				AddDomainEvent(
						new AuthorAssigned(author, action));
				AddAction(action);
		}

		public void Approve(IArticleAction<ArticleActionType> action, ArticleStateMachineFactory _stateMachineFactory)
		{
				Actors.Add(new ArticleActor()
				{
						PersonId = action.CreatedById,
						Role = UserRoleType.REVED
				});

				SetStage(ArticleStage.InitialApproved, action, _stateMachineFactory);
				
				AddDomainEvent(new ArticleApproved(this, action));
		}

		public void Reject(IArticleAction<ArticleActionType> action, ArticleStateMachineFactory _stateMachineFactory)
		{
				SetStage(ArticleStage.InitialRejected, action, _stateMachineFactory);

				AddDomainEvent(new ArticleRejected(this, action));
		}

		public void Submit(IArticleAction<ArticleActionType> action, ArticleStateMachineFactory _stateMachineFactory)
		{
				SubmittedById = action.CreatedById;
				SubmittedOn = action.CreatedOn;

				SetStage(ArticleStage.Submitted, action, _stateMachineFactory);
		}

		public Asset CreateAsset(AssetTypeDefinition type, IArticleAction<ArticleActionType> action)
		{
				var assetCount = _assets
						.Where(a => a.Type == type.Id)
						.Count();

				if (assetCount >= type.MaxAssetCount)
						throw new DomainException($"The maximum number of files, {type.MaxAssetCount}, allowed for {type.Name.ToString()} was already reached");

				var asset = Asset.Create(this, type);
				_assets.Add(asset);

				AddAction(action);
				return asset;
		}

		private void AddAction(IArticleAction<ArticleActionType> action)
		{
				_actions.Add(action.Adapt<ArticleAction>());
				AddDomainEvent(new ArticleActionExecuted(this, action));
		}
}
