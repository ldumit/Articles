using Flurl;
using Blocks.AspNetCore;
using EmailService.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using EmailAddress = EmailService.Contracts.EmailAddress;
using Microsoft.Extensions.Options;

namespace Review.Application.Features.Invitations.InviteReviewer;

public class InviteReviewerHandler(ArticleRepository _articleRepository, IClaimsProvider _claimsProvider, IEmailService _emailService, IHttpContextAccessor _httpContextAccessor, IOptions<EmailOptions> emailOptions)
                : IRequestHandler<InviteReviewerCommand, IdResponse>
{
    public async Task<IdResponse> Handle(InviteReviewerCommand command, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

        var reviewer = await _articleRepository.Context.Reviewers.SingleOrDefaultAsync(r => command.Email.Equals(r.Email, StringComparison.CurrentCultureIgnoreCase));
        var editor = await _articleRepository.Context.Reviewers.SingleAsync(r => r.UserId == _claimsProvider.GetUserId());

        var invitation = new ReviewInvitation
        {
            ArticleId = article.Id,
            EmailAddress = command.Email,
            FullName = command.FullName,
            SentById = _claimsProvider.GetUserId(),
            ExpiresOn = DateTime.UtcNow.AddDays(7),
            Token = Guid.NewGuid().ToString(),
        };

        article.Invitations.Add(invitation);
        await _articleRepository.SaveChangesAsync();

        await _emailService.SendEmailAsync(BuildEmailMessage(invitation, editor));


        return new IdResponse(article.Id);
    }

    private EmailMessage BuildEmailMessage(ReviewInvitation invitation, Reviewer editor)
    {
        const string InvitationEmail =
                @"Dear Contributor, 
						You've been invited by {0} to review the following article: {1}.
						Please accept or deny, the invitation will expire on {2}.
						If you don't have an account please create one using the following URL: {3}";

        var url =
                _httpContextAccessor.HttpContext?.Request.BaseUrl()
                .AppendPathSegment($"articles/{invitation.ArticleId}/invitations/{invitation.Token}/status");
        //.SetQueryParams(new { token });

        return new EmailMessage(
                "Review Invitation",
                new Content(ContentType.Html, string.Format(InvitationEmail, editor.FirstName + " " + editor.LastName, url, invitation.ExpiresOn, url)),
                new EmailAddress("articles", emailOptions.Value.EmailFromAddress),
                new List<EmailAddress> { new EmailAddress(invitation.FullName, invitation.EmailAddress) }
                );
    }
}
