using Auth.Grpc;
using Microsoft.EntityFrameworkCore;
using static Auth.Grpc.AuthService;

namespace Review.Application.Features.Invitations.AcceptInvitation;

public class AcceptInvitationCommandHandler(ArticleRepository _articleRepository, AuthServiceClient _authClient)
    : IRequestHandler<AcceptInvitationCommand, IdResponse>
{
    public async Task<IdResponse> Handle(AcceptInvitationCommand command, CancellationToken ct)
    {
        var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);
        var invitation = await _articleRepository.Context.ReviewInvitations
						.SingleOrThrowAsync(i => command.Token.Equals(i.Token) && i.Status == InvitationStatus.Open);
        
				//if we have the UserId in the Invitaion use it, if not use the Current UserId
				var reviewer = await _articleRepository.Context.Reviewers.SingleOrDefaultAsync(r => r.UserId == (invitation.UserId ?? command.CreatedById));
        if(reviewer is null)
						reviewer = await CreateReviewerFromUser(command, ct);

        // since we can assign a reviewer directly without sending the invitation, changing the invitation status should staty separetly 
        invitation.Accept();

				article.AssignReviewer(reviewer, command);

        await _articleRepository.SaveChangesAsync();

        return new IdResponse(article.Id);
    }

		// This logic may be extracted into ReviewerFactory if reused later
		private async Task<Reviewer> CreateReviewerFromUser(AcceptInvitationCommand command, CancellationToken ct)
		{
				var reviewer = await _articleRepository.Context.Reviewers.FirstOrDefaultAsync(x => x.UserId == command.CreatedById, ct);
				if (reviewer is null)
				{
						var response = _authClient.GetUserById(new GetUserRequest { UserId = command.CreatedById });
						var userInfo = response.UserInfo;
						reviewer = Reviewer.Create(userInfo.Email, userInfo.FirstName, userInfo.LastName, userInfo.Honorific, userInfo.Affiliation, command);

						//author = response.UserInfo.Adapt<Author>();
						await _articleRepository.Context.Reviewers.AddAsync(reviewer, ct);
				}

				return reviewer;
		}
}
