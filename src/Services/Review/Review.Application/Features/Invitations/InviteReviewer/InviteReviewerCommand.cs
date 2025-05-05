using Review.Application.Features.Articles._Shared;

namespace Review.Application.Features.Invitations.InviteReviewer;

public record InviteReviewerCommand(string FullName, string Email)
        : ArticleCommand<ArticleActionType, IdResponse>
{
    public override ArticleActionType ActionType => ArticleActionType.InviteReviewer;
}

public class CreateArticleCommandValidator : AbstractValidator<InviteReviewerCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmptyWithMessage(nameof(InviteReviewerCommand.Email))
            .MaximumLengthWithMessage(MaxLength.C256, nameof(InviteReviewerCommand.Email));
    }
}
