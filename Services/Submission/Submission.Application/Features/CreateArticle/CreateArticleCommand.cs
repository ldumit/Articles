using FluentValidation;
using MediatR;
using Submission.Application.Features.Shared;
using Submission.Domain.Enums;

namespace Submission.Application.Features.CreateArticle;

public record CreateArticleCommand(int JournalId, string Title, ArticleType Type, string ScopeStatement):
		ArticleCommand<CreateArticleResponse>
{
		public override ArticleActionType ActionType => ArticleActionType.Create;
}

public record CreateArticleResponse(int Id);

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
		public CreateArticleCommandValidator()
		{
				RuleFor(x => x.Title)
						.NotEmpty().WithMessage("Title is required.")
						.MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

				RuleFor(x => x.ScopeStatement)
						.NotEmpty().WithMessage("Summary is required.");

				RuleFor(x => x.JournalId)
						.GreaterThan(0).WithMessage("JournalId is required.");
		}
}
