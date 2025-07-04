using Mapster;
using Blocks.Core.Security;
using Blocks.Domain;
using Review.Domain.StateMachines;
using Articles.Abstractions.Events.Dtos;

namespace Review.Domain.Entities;

public partial class Article
{
		public void SetStage(ArticleStage newStage, ArticleStateMachineFactory stateMachineFactory, IArticleAction action)
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

		public void AssignEditor(Editor actor, IArticleAction action)
		{
				if (_actors.Exists(a => a.PersonId == actor.Id && a.Role == UserRoleType.REVED))
						throw new DomainException($"Editor {actor.Email} is already assigned to the article");

				if (_actors.Exists(a => a.Role == UserRoleType.REVED))
						throw new DomainException($"An Editor is already assigned to the article");

				_actors.Add(new ArticleActor() { PersonId = actor.Id, Role = UserRoleType.REVED });

				AddDomainEvent(
						new EditorAssigned(actor.Id, actor.UserId!.Value, action));
				
				AddAction(action);
		}

		public void AssignReviewer(Reviewer actor, IArticleAction action)
		{
				if (_actors.Exists(a => a.PersonId == actor.Id && a.Role == UserRoleType.REV))
						throw new DomainException($"Reviewer {actor.Email} is already assigned to the article");

				actor.AddSpecialization(new ReviewerSpecialization() { JournalId = this.JournalId, ReviewerId = actor.Id});

				_actors.Add(new ArticleActor() { PersonId = actor.Id, Role = UserRoleType.REV });

				AddDomainEvent(
						new ReviewerAssigned(actor.Id, actor.UserId!.Value, action));

				AddAction(action);
		}

		public void AssignActor(int personId, UserRoleType roleType)
		{
				_actors.Add(new ArticleActor { ArticleId = this.Id, PersonId = personId, Role = roleType });
		}

		public void Accept(ArticleStateMachineFactory _stateMachineFactory, IArticleAction action)
		{
				SetStage(ArticleStage.Accepted, _stateMachineFactory, action);
				
				AddDomainEvent(new ArticleAccepted(this, action));
		}

		public void Reject(ArticleStateMachineFactory _stateMachineFactory, IArticleAction action)
		{
				SetStage(ArticleStage.Rejected, _stateMachineFactory, action);

				AddDomainEvent(new ArticleRejected(this, action));
		}

		public Asset CreateAsset(AssetTypeDefinition type, IArticleAction action)
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

		public ReviewInvitation InviteReviewer(Reviewer reviewer, IArticleAction action)
		{
				if (!reviewer.Specializations.Any(s => s.JournalId == this.JournalId))
						throw new DomainException($"Reviewer {reviewer.FullName} is not specialized in article's journal");

				return CreateInvitation(reviewer.UserId, reviewer.Email.Value, reviewer.FullName, action: action);
		}

		public ReviewInvitation InviteReviewer(int? userId, string email, string fullName, IArticleAction action)
		{
				return CreateInvitation(userId, email, fullName, action);
		}

		private ReviewInvitation CreateInvitation(int? userId, string email, string fullName, IArticleAction action)
		{
				// check if there is an active invitation for this email
				if (_invitations.Any(i =>
								i.EmailAddress.Trim().ToUpperInvariant() == email.Trim().ToUpperInvariant()
								&& !i.IsExpired)) 
						throw new DomainException($"Reviewer {fullName} ({email}) was already invited");

				var invitation = new ReviewInvitation
				{
						ArticleId = this.Id,
						EmailAddress = email,
						FullName = fullName,
						SentById = action.CreatedById,
						ExpiresOn = DateTime.UtcNow.AddDays(7),
						Token = Base64UrlTokenGenerator.Generate(),
				};

				AddDomainEvent(new ReviewerInvited(invitation, action));

				_invitations.Add(invitation);
				return invitation;
		}

		private void AddAction(IArticleAction action)
		{
				_actions.Add(action.Adapt<ArticleAction>());
				AddDomainEvent(new ArticleActionExecuted(this, action));
		}

		public static Article AcceptSubmitted(ArticleDto articleDto)
		{
				return new Article
				{
						JournalId = articleDto.Journal.Id,
						Title = articleDto.Title,
						Type = articleDto.Type,
						Scope = articleDto.Scope,
						SubmittedById = articleDto.SubmittedBy.Id,
						SubmittedOn = articleDto.SubmittedOn,
						Stage = articleDto.Stage,
				};
		}

		public static TPerson CreatePerson<TPerson>(PersonDto person)
				where TPerson : Person, new()
		{
				return new TPerson
				{
						FirstName = person.FirstName,
						LastName = person.LastName,
						Honorific = person.Honorific,
						Affiliation = person.Affiliation,
						Email = person.Email,
						UserId = person.UserId,
				};
		}
}
