using Blocks.AspNetCore;
using EmailService.Contracts;
using Flurl;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Review.Domain.Events;
using EmailAddress = EmailService.Contracts.EmailAddress;

namespace Review.Application.Features.Invitations.InviteReviewer;

public class SendInvitationEmailOnReviewerInvitedHandler(ArticleRepository _articleRepository, IEmailService _emailService, IHttpContextAccessor _httpContextAccessor, IOptions<EmailOptions> emailOptions)
		: INotificationHandler<ReviewerInvited>
{
		public async Task Handle(ReviewerInvited notification, CancellationToken ct)
		{
				var article = await _articleRepository.GetFullArticleById(notification.Invitation.ArticleId);
				//await _emailService.SendEmailAsync(BuildEmailMessage(invitation, editor));

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
								.AppendPathSegment($"articles/{invitation.ArticleId}/invitations/{invitation.Token}/status");

				return new EmailMessage(
								"Review Invitation",
								new Content(ContentType.Html, string.Format(InvitationEmail, editor.FirstName + " " + editor.LastName, url, invitation.ExpiresOn, url)),
								new EmailAddress("articles", emailOptions.Value.EmailFromAddress),
								new List<EmailAddress> { new EmailAddress(invitation.FullName, invitation.Email) }
								);
		}
}
