using Blocks.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace Review.Application.Features.Invitations.AcceptInvitation;

public class AcceptInvitationCommandHandler(ArticleRepository _articleRepository, IClaimsProvider _claimsProvider)
    : IRequestHandler<AcceptInvitationCommand, IdResponse>
{
    public async Task<IdResponse> Handle(AcceptInvitationCommand command, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);
        var invitation = await _articleRepository.Context.ReviewInvitations.SingleOrThrowAsync(i => command.Token.Equals(i.Token));
        var reviewer = await _articleRepository.Context.Reviewers.SingleOrDefaultAsync(r => r.UserId == _claimsProvider.GetUserId());
        if(reviewer is null)
        {
            //todo Get user information from Auth service using gRPC
            //reviewer = new Reviewer { }
        }

        // since we can assign a reviewer directly without sending the invitation, changing the invitation status should staty separetly 
        invitation.Status = InvitationStatus.Accepted;
        article.AssignReviewer(reviewer, command);

        await _articleRepository.SaveChangesAsync();

        return new IdResponse(article.Id);
    }
}
