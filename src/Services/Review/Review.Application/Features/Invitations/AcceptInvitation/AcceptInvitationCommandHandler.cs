using Auth.Grpc;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Review.Persistence;

namespace Review.Application.Features.Invitations.AcceptInvitation;

public class AcceptInvitationCommandHandler(ReviewDbContext _dbContext, ArticleRepository _articleRepository, IPersonService _personClient)
    : IRequestHandler<AcceptInvitationCommand, IdResponse>
{
    public async Task<IdResponse> Handle(AcceptInvitationCommand command, CancellationToken ct)
    {
        var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);
        var invitation = await _dbContext.ReviewInvitations
						.SingleOrThrowAsync(i => command.Token.Equals(i.Token) && i.Status == InvitationStatus.Open);

				Reviewer? reviewer = default!;
				if(invitation.UserId != null) 
						reviewer = await _dbContext.Reviewers.SingleOrDefaultAsync(r => r.UserId == invitation.UserId);

				if (reviewer is null)
						reviewer = await CreateReviewerFromPerson(invitation, command, ct);

        // since we can't assign a reviewer directly without sending the invitation, changing the invitation status should stay separetly 
        invitation.Accept();

				article.AssignReviewer(reviewer, command);

        await _articleRepository.SaveChangesAsync();

        return new IdResponse(article.Id);
    }

		// This logic may be extracted into ReviewerFactory if reused later
		private async Task<Reviewer> CreateReviewerFromPerson(ReviewInvitation reviewInvitation, AcceptInvitationCommand command, CancellationToken ct)
		{
				PersonInfo? userInfo = default!;
				if(reviewInvitation.UserId != null)
						userInfo = (await _personClient.GetPersonByUserIdAsync(new GetPersonByUserIdRequest { UserId = command.CreatedById }, new CallOptions(cancellationToken: ct))).PersonInfo;
				
				if(userInfo is null)
				{
						//todo - implement CreateUser grpcs and return personInfo
						throw new NotImplementedException("CreateUser is not implemented");
				}

				var reviewer = Reviewer.Create(userInfo, command);

				await _dbContext.Reviewers.AddAsync(reviewer, ct);
				return reviewer;
		}
}
