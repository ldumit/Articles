using Review.Application.Features.Articles._Shared;

namespace Review.Application.Features.Articles.CreateAuthor;

public record CreateAuthorCommand(
        string Email,
        string FirstName,
        string LastName,
        string? Title,
        string Affiliation) : ArticleCommand
{
    public override ArticleActionType ActionType => ArticleActionType.CreateAuthor;
}

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(x => x.Email)
                .NotEmptyWithMessage(nameof(CreateAuthorCommand.Email))
                .MaximumLengthWithMessage(Constraints.C64, nameof(CreateAuthorCommand.Email));

        RuleFor(x => x.FirstName)
                .NotEmptyWithMessage(nameof(CreateAuthorCommand.FirstName))
                .MaximumLengthWithMessage(Constraints.C64, nameof(CreateAuthorCommand.FirstName));

        RuleFor(x => x.LastName)
                .NotEmptyWithMessage(nameof(CreateAuthorCommand.LastName))
                .MaximumLengthWithMessage(Constraints.C64, nameof(CreateAuthorCommand.LastName));

        RuleFor(x => x.Title)
                .MaximumLengthWithMessage(Constraints.C64, nameof(CreateAuthorCommand.Title));

        RuleFor(x => x.Affiliation)
                .NotEmptyWithMessage(nameof(CreateAuthorCommand.Affiliation))
                .MaximumLengthWithMessage(Constraints.C512, nameof(CreateAuthorCommand.Affiliation));
    }
}