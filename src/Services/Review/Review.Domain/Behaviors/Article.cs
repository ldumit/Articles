using Blocks.Core.Security;
using Blocks.Domain;
using Mapster;
using Review.Domain.StateMachines;

namespace Review.Domain.Entities;

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
						new ArticleStageChanged(action, currentStage, newStage));
    }

		public void AssignEditor(Editor actor, IArticleAction<ArticleActionType> action)
		{
				if (_actors.Exists(a => a.PersonId == actor.Id && a.Role == UserRoleType.REVED))
						throw new DomainException($"Editor {actor.Email} is already assigned to the article");

				if (_actors.Exists(a => a.Role == UserRoleType.REVED))
						throw new DomainException($"An Editor is already assigned to the article");

				_actors.Add(new ArticleActor() { PersonId = actor.Id, Role = UserRoleType.REVED });

				AddDomainEvent(
						new EditorAssigned(action, actor.Id, actor.UserId!.Value));
				
				AddAction(action);
		}

		public void AssignReviewer(Reviewer actor, IArticleAction<ArticleActionType> action)
		{
				if (_actors.Exists(a => a.PersonId == actor.Id && a.Role == UserRoleType.REV))
						throw new DomainException($"Reviewer {actor.Email} is already assigned to the article");

				_actors.Add(new ArticleActor() { PersonId = actor.Id, Role = UserRoleType.REV });

				AddDomainEvent(
						new ReviewerAssigned(action, actor.Id, actor.UserId!.Value));

				AddAction(action);
		}

		public void AssignActor(int personId, UserRoleType roleType)
		{
				_actors.Add(new ArticleActor { ArticleId = this.Id, PersonId = personId, Role = roleType });
		}


		//public void InviteReviewer(string emailAddress, IArticleAction<ArticleActionType> action)
		//{
		//		if (_contributors.Exists(a => a.PersonId == contributor.Id && a.Role == UserRoleType.REV))
		//				throw new DomainException($"Reviewer {contributor.Email} is already assigned to the article");

		//		_contributors.Add(new ArticleContributor() { PersonId = contributor.Id, Role = UserRoleType.REV });

		//		var invitation = new ReviewInvitation { ArticleId = Id, EmailAddress = Email, SentById = _claimsProvider.GetUserId(), ExpiresOn = DateTime.UtcNow.AddDays(7) };


		//		AddDomainEvent(
		//				new ReviewerAssigned(action, contributor.Id, contributor.UserId!.Value));

		//		AddAction(action);
		//}

		public void Accept(IArticleAction<ArticleActionType> action, ArticleStateMachineFactory _stateMachineFactory)
		{
				SetStage(ArticleStage.Accepted, action, _stateMachineFactory);
				
				AddDomainEvent(new ArticleAccepted(this, action));
		}

		public void Reject(IArticleAction<ArticleActionType> action, ArticleStateMachineFactory _stateMachineFactory)
		{
				SetStage(ArticleStage.Rejected, action, _stateMachineFactory);

				AddDomainEvent(new ArticleRejected(this, action));
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

		public ReviewInvitation AddReviewInvitation(int? userId, string emailAddress, string fullName, IArticleAction<ArticleActionType> action)
		{
				var invitation = new ReviewInvitation
				{
						ArticleId = this.Id,
						EmailAddress = emailAddress,
						FullName = fullName,
						SentById = action.CreatedById,
						ExpiresOn = DateTime.UtcNow.AddDays(7),
						Token = Base64UrlTokenGenerator.Generate(),
				};

				_invitations.Add(invitation);
				return invitation;
		}

		private void AddAction(IArticleAction<ArticleActionType> action)
		{
				_actions.Add(action.Adapt<ArticleAction>());
				AddDomainEvent(new ArticleActionExecuted(action, this));
		}

}
