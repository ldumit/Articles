using Blocks.FluentValidation;
using Blocks.Core;
using FluentValidation;
using Submission.Application.Features.Shared;
using Submission.Domain.Enums;

namespace Submission.Application.Features.AssignAuthor;

public record AssignAuthorCommand(int AuthorId, bool IsCorrespondingAuthor, HashSet<ContributionArea> ContributionAreas)
		: ArticleCommand
{
		public override ArticleActionType ActionType => ArticleActionType.AssignAuthor;
}

public class AssignAuthorCommandValidator : ArticleCommandValidator<AssignAuthorCommand>
{
		public AssignAuthorCommandValidator()
		{
				RuleFor(c => c.AuthorId).GreaterThan(0).WithMessageForInvalidId(nameof(AssignAuthorCommand.AuthorId));

				RuleFor(command => command.ContributionAreas)
						.Must(HasMandatoryContribution)
						.WithMessage("The author must contribute to at least one mandatory area.");
		}

		private bool HasMandatoryContribution(HashSet<ContributionArea> contributionAreas)
				=> contributionAreas.Overlaps(ContributionAreaCategories.MandatoryAreas);
}