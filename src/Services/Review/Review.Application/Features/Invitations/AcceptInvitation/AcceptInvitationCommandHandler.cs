using Auth.Grpc;
using Microsoft.EntityFrameworkCore;
using Review.Domain.Reviewers;
using Review.Persistence;

namespace Review.Application.Features.Invitations.AcceptInvitation;

public class AcceptInvitationCommandHandler(ReviewDbContext _dbContext, ArticleRepository _articleRepository, ReviewInvitationRepositoryy _reviewInivtiationRepository, IPersonService _personClient)
    : IRequestHandler<AcceptInvitationCommand, IdResponse>
{
    public async Task<IdResponse> Handle(AcceptInvitationCommand command, CancellationToken ct)
    {
        var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);
        var invitation = await _reviewInivtiationRepository.GetByTokenOrThrow(command.Token);

				Reviewer? reviewer = default!;
				if (invitation.UserId != null)
				{
						reviewer = await _dbContext.Reviewers.SingleOrDefaultAsync(r => r.UserId == invitation.UserId);
						if (reviewer is null)
								reviewer = await CreateReviewerByUserId(invitation.UserId.Value, article, command, ct);
				}
				else
						throw new NotImplementedException("The reviewer doesn't exist as a user, create an account first.");


        // keeping those 2 methods separatly, allows us to assign reviewers directly without an invitation
        invitation.Accept();
				article.AssignReviewer(reviewer, command);

        await _articleRepository.SaveChangesAsync();

        return new IdResponse(invitation.Id);
    }

		private async Task<Reviewer> CreateReviewerByUserId(int userId, Article article, Domain.Shared.IArticleAction command, CancellationToken ct)
		{
				var response = await _personClient.GetPersonByUserIdAsync(new GetPersonByUserIdRequest { UserId = userId });
				var reviewer = Reviewer.Create(response.PersonInfo, new HashSet<int>(article.JournalId), command);
				await _dbContext.Reviewers.AddAsync(reviewer, ct);

				return reviewer;
		}
}
