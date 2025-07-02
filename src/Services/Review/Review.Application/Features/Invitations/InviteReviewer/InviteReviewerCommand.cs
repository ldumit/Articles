using Review.Application.Features.Articles._Shared;

namespace Review.Application.Features.Invitations.InviteReviewer;

public record InviteReviewerCommand(int? UserId, string FirstName, string LastName, string Email)
        : ArticleCommand<IdResponse>
{
		public override ArticleActionType ActionType => ArticleActionType.InviteReviewer;
		public string FullName => FirstName + ' ' + LastName;
}

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
								.NotEmptyWithMessage(nameof(InviteReviewerCommand.LastName))
								.MaximumLengthWithMessage(MaxLength.C256, nameof(InviteReviewerCommand.LastName));
				});
    }
}
