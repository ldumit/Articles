using Auth.Grpc;
using Blocks.AspNetCore;
using EmailService.Contracts;
using Flurl;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Review.Persistence;
using EmailAddress = EmailService.Contracts.EmailAddress;

namespace Review.Application.Features.Invitations.InviteReviewer;

public class InviteReviewerCommandHandler(
    ReviewDbContext _dbContext,
    ArticleRepository _articleRepository, 
    ReviewerRepository _reviewRepository, 
    IEmailService _emailService,
    IPersonService _personClient,
		IHttpContextAccessor _httpContextAccessor, 
    IOptions<EmailOptions> emailOptions)
    : IRequestHandler<InviteReviewerCommand, InviteReviewerResponse>
{
    public async Task<InviteReviewerResponse> Handle(InviteReviewerCommand command, CancellationToken ct)
    {
        var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);
				var editor = await _dbContext.Editors.SingleAsync(r => r.UserId == command.CreatedById);

        ReviewInvitation invitation = default!;
        if (command.UserId != null)
        {
            var reviewer = await _reviewRepository.GetByUserIdAsync(command.UserId.Value);
            if (reviewer is not null)
            {
                invitation = article.InviteReviewer(reviewer, command);
            }
            else
            {
                var personInfo = await GetPersonByUserId(command.UserId.Value, ct);
                invitation = article.InviteReviewer(personInfo.UserId, personInfo.Email, personInfo.FirstName, personInfo.LastName, command);
            }
        }
        else
        {
            invitation = article.InviteReviewer(command.UserId, command.Email, command.FirstName, command.LastName, command);
        }


				await _articleRepository.SaveChangesAsync();

        // todo - decide if it is necessary here a domain event or not
        await _emailService.SendEmailAsync(BuildEmailMessage(invitation, editor));

        return new InviteReviewerResponse(article.Id, invitation.Id, invitation.Token.Value);
    }

    private EmailMessage BuildEmailMessage(ReviewInvitation invitation, Editor editor)
    {
        const string InvitationEmail =
                @"Dear Contributor, 
						You've been invited by {0} to review the following article: {1}.
						Please accept or deny, the invitation will expire on {2}.
						If you don't have an account please create one using the following URL: {3}";

        var url =
                _httpContextAccessor.HttpContext?.Request.BaseUrl()
                .AppendPathSegment($"articles/{invitation.ArticleId}/invitations/{invitation.Token}");

        return new EmailMessage(
                "Review Invitation",
                new Content(ContentType.Html, string.Format(InvitationEmail, editor.FirstName + " " + editor.LastName, url, invitation.ExpiresOn, url)),
                new EmailAddress("articles", emailOptions.Value.EmailFromAddress),
                new List<EmailAddress> { new EmailAddress(invitation.FullName, invitation.Email) }
                );
    }

		private async Task<PersonInfo> GetPersonByUserId(int userId, CancellationToken ct)
		{
				var response = await _personClient.GetPersonByUserIdAsync(new GetPersonByUserIdRequest { UserId = userId }, ct);
        return response.PersonInfo;
		}
}
