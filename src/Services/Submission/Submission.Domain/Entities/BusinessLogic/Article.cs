using Articles.Abstractions;
using Articles.Exceptions.Domain;
using Articles.Security;
using Submission.Domain.Enums;
using Submission.Domain.Events;
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

				_stageHistories.Add(new StageHistory { ArticleId = Id, StageId = newStage, StartDate = DateTime.UtcNow });
				AddDomainEvent(
            new ArticleStageChangedDomainEvent(action, currentStage, newStage)
            );
    }

		public void AssignAuthor(Author author, HashSet<ContributionArea> contributionAreas, bool isCorrespondingAuthor,  IAction action)
		{
				var role = isCorrespondingAuthor ? UserRoleType.CORAUT : UserRoleType.AUT;				
				
				if (Actors.Exists(a => a.PersonId == author.Id && a.Role == role))
						throw new DomainException($"Author {author.Email} is already assigned to the article");

				Actors.Add(new AuthorActor() {
						ContributionAreas = contributionAreas,
						PersonId = author.Id, 
						Role = role
				});

				//AddDomainEvent(new TypesetterAssignedDomainEvent(action, typesetter.Id, typesetter.UserId!.Value));
				//AuthorId = authorId;
				//ContributionAreas = contributionAreas;
				//SetStage(ArticleStage.AuthorAssigned, action);
		}

		public Asset CreateAsset(AssetTypeDefinition type)
    {
				byte? assetNumber = _assets
						.Where(a => a.Type == type.Id)
						.Select(a => (byte?)a.Number) // Cast to byte? to allow nulls
						.Max(); 
				if(assetNumber is not null)
						assetNumber += 1;
				else
						assetNumber = type.AllowsMultipleAssets ? (byte)1 : (byte)0;

				var asset = Asset.Create(this, type, assetNumber.Value);
        _assets.Add(asset);        
        return asset;
    }
}
