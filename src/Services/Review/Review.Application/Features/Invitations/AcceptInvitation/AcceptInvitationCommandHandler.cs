using Auth.Grpc;
using Review.Domain.Reviewers;

namespace Review.Application.Features.Invitations.AcceptInvitation;

public class AcceptInvitationCommandHandler(ArticleRepository _articleRepository, ReviewerRepository _reviewerRepository, ReviewInvitationRepository _reviewInivtiationRepository, IPersonService _personClient)
    : IRequestHandler<AcceptInvitationCommand, AcceptInvitationResponse>
{
    public async Task<AcceptInvitationResponse> Handle(AcceptInvitationCommand command, CancellationToken ct)
    {
        var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

				var invitation = await _reviewInivtiationRepository.GetByTokenOrThrow(command.Token);

				Reviewer? reviewer = default!;
				if (invitation.UserId != null)
				{
						command.CreatedById = invitation.UserId.Value;
						reviewer = await _reviewerRepository.GetByUserIdAsync(invitation.UserId.Value);
						if (reviewer is null)
								reviewer = await CreateReviewerByUserId(invitation.UserId.Value, article, command, ct);
				}
				else
						throw new NotImplementedException("The reviewer doesn't exist as a user, create an account first.");

        //keeping those 2 methods separatly, allows us to assign reviewers directly without an invitation
        invitation.Accept();
				article.AssignReviewer(reviewer, command);

        await _articleRepository.SaveChangesAsync();

        return new AcceptInvitationResponse(article.Id, invitation.Id, reviewer.Id);
    }

		private async Task<Reviewer> CreateReviewerByUserId(int userId, Article article, IArticleAction command, CancellationToken ct)
		{
				var response = await _personClient.GetPersonByUserIdAsync(new GetPersonByUserIdRequest { UserId = userId });
				var reviewer = Reviewer.Create(response.PersonInfo, new HashSet<int> { article.JournalId }, command);
				await _reviewerRepository.AddAsync(reviewer, ct);

				return reviewer;
		}
}
