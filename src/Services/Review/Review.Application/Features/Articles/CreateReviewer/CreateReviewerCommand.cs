using Review.Application.Features.Articles._Shared;

namespace Review.Application.Features.Articles.CreateReviewer;

public record CreateReviewerCommand(
        string Email,
        string FirstName,
        string LastName,
        string? Title,
        string Affiliation) : ArticleCommand
{
    public override ArticleActionType ActionType => ArticleActionType.CreateReviewer;
}

public class CreateReviewerCommandValidator : AbstractValidator<CreateReviewerCommand>
{
    public CreateReviewerCommandValidator()
    {
        RuleFor(x => x.Email)
                .NotEmptyWithMessage(nameof(CreateReviewerCommand.Email))
                .MaximumLengthWithMessage(MaxLength.C64, nameof(CreateReviewerCommand.Email));

        RuleFor(x => x.FirstName)
                .NotEmptyWithMessage(nameof(CreateReviewerCommand.FirstName))
                .MaximumLengthWithMessage(MaxLength.C64, nameof(CreateReviewerCommand.FirstName));

        RuleFor(x => x.LastName)
                .NotEmptyWithMessage(nameof(CreateReviewerCommand.LastName))
                .MaximumLengthWithMessage(MaxLength.C64, nameof(CreateReviewerCommand.LastName));

        RuleFor(x => x.Title)
                .MaximumLengthWithMessage(MaxLength.C64, nameof(CreateReviewerCommand.Title));

        RuleFor(x => x.Affiliation)
                .NotEmptyWithMessage(nameof(CreateReviewerCommand.Affiliation))
                .MaximumLengthWithMessage(MaxLength.C512, nameof(CreateReviewerCommand.Affiliation));
    }
}