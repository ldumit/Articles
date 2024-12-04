namespace Review.Application.Features.InviteReviewer;

public record InviteReviewerCommand(int JournalId, string Title, ArticleType Type, string Scope)
		: ArticleCommand<ArticleActionType, IdResponse>
{
		public override ArticleActionType ActionType => ArticleActionType.Create;
}

public class CreateArticleCommandValidator : AbstractValidator<InviteReviewerCommand>
{
		public CreateArticleCommandValidator()
		{
				RuleFor(x => x.Title)
						.NotEmptyWithMessage(nameof(InviteReviewerCommand.Title))
						.MaximumLengthWithMessage(Constraints.C256, nameof(InviteReviewerCommand.Title));

				RuleFor(x => x.Scope)
						.NotEmptyWithMessage(nameof(InviteReviewerCommand.Scope))
						.MaximumLengthWithMessage(Constraints.C2048, nameof(InviteReviewerCommand.Scope));

				RuleFor(c => c.JournalId).GreaterThan(0).WithMessageForInvalidId(nameof(InviteReviewerCommand.JournalId));
		}
}
