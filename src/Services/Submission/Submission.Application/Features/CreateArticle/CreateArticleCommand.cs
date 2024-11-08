using FluentValidation;
using Submission.Application.Features.Shared;
using Submission.Domain.Enums;

namespace Submission.Application.Features.CreateArticle;

public record CreateArticleCommand(int JournalId, string Title, ArticleType Type, string Scope) 
		: ArticleCommand
{
		public override ArticleActionType ActionType => ArticleActionType.Create;
}

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
		public CreateArticleCommandValidator()
		{
				RuleFor(x => x.Title)
						.NotEmpty().WithMessage("Title is required.")
						.MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

				RuleFor(x => x.Scope)
						.NotEmpty().WithMessage("Summary is required.");

				RuleFor(x => x.JournalId)
						.GreaterThan(0).WithMessage("JournalId is required.");
		}
}
