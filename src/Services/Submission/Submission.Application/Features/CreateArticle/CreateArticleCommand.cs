using FluentValidation;
using Articles.FluentValidation;
using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Articles.EntityFrameworkCore;
using Submission.Application.Features.Shared;
using Submission.Domain.Enums;

namespace Submission.Application.Features.CreateArticle;

public record CreateArticleCommand(int JournalId, string Title, ArticleType Type, string Scope)
		: ArticleCommand<ArticleActionType, IdResponse>
{
		public override ArticleActionType ActionType => ArticleActionType.Create;
}

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
		public CreateArticleCommandValidator()
		{
				RuleFor(x => x.Title)
						.NotEmptyWithMessage(nameof(CreateArticleCommand.Title))
						.MaximumLengthWithMessage(Constraints.C256, nameof(CreateArticleCommand.Title));

				RuleFor(x => x.Scope)
						.NotEmptyWithMessage(nameof(CreateArticleCommand.Scope))
						.MaximumLengthWithMessage(Constraints.C2048, nameof(CreateArticleCommand.Scope));

				RuleFor(c => c.JournalId).GreaterThan(0).WithMessageForInvalidId(nameof(CreateArticleCommand.JournalId));
		}
}
