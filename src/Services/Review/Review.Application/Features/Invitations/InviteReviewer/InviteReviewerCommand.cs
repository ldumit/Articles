using Review.Application.Features.Articles._Shared;
using Review.Domain.Articles.Enums;

namespace Review.Application.Features.Invitations.InviteReviewer;

public record InviteReviewerCommand(int? UserId, string FirstName, string LastName, string Email)
        : ArticleCommand<InviteReviewerResponse>
{
		public override ArticleActionType ActionType => ArticleActionType.InviteReviewer;
}

public record InviteReviewerResponse(int ArticleId, int InvitationId, string Token);

public class CreateArticleCommandValidator : AbstractValidator<InviteReviewerCommand>
{
    public CreateArticleCommandValidator()
    {
				When(c => c.UserId == null, () =>
				{ 
						RuleFor(x => x.Email)
								.NotEmptyWithMessage(nameof(InviteReviewerCommand.Email))
								.MaximumLengthWithMessage(MaxLength.C64, nameof(InviteReviewerCommand.Email))
								.EmailAddress();

						RuleFor(x => x.FirstName)
								.NotEmptyWithMessage(nameof(InviteReviewerCommand.FirstName))
								.MaximumLengthWithMessage(MaxLength.C64, nameof(InviteReviewerCommand.FirstName));

						RuleFor(x => x.LastName)
								.NotEmpty()
								.MaximumLength(MaxLength.C256);
								//.NotEmptyWithMessage(nameof(InviteReviewerCommand.LastName))
								//.MaximumLengthWithMessage(MaxLength.C256, nameof(InviteReviewerCommand.LastName));
				});
    }
}
