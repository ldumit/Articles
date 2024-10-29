using FluentValidation;
using Submission.Application.Features.Shared;
using Submission.Domain.Enums;

namespace Submission.Application.Features.ApproveArticle;

public record ApproveArticleCommand : ArticleCommand
{
		public override ArticleActionType ActionType => ArticleActionType.Approve;
}


public class ApproveArticleCommandValidator : AbstractValidator<ApproveArticleCommand>
{
		public ApproveArticleCommandValidator()
		{
		}
}
