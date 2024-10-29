using FluentValidation;
using Submission.Application.Features.Shared;
using Submission.Domain.Enums;

namespace Submission.Application.Features.AssignAuthor;

public record AssignAuthorCommand(int AuthorId, bool IsCorrespondingAuthor, List<ContributionArea> ContributionAreas)
		: ArticleCommand
{
		public override ArticleActionType ActionType => ArticleActionType.AssignAuthor;
}

//todo - add validation rules for all commands and integrate it with MediatR pipeline
public class AssignAuthorCommandValidator : AbstractValidator<AssignAuthorCommand>
{
		public AssignAuthorCommandValidator()
		{				
		}
}