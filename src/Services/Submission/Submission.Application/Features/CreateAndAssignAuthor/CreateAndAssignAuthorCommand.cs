namespace Submission.Application.Features.CreateAndAssignAuthor;

public record CreateAndAssignAuthorCommand(int? UserId, string? FirstName, string? LastName, string? Email, string? Title, string? Affiliation, bool IsCorrespondingAuthor, HashSet<ContributionArea> ContributionAreas)
		: ArticleCommand
{
		public override ArticleActionType ActionType => ArticleActionType.AssignAuthor;
}

public class CreateAndAssignAuthorCommandValidator : ArticleCommandValidator<CreateAndAssignAuthorCommand>
{
		public CreateAndAssignAuthorCommandValidator()
		{
				When(c => c.UserId == null, () =>
				{
						RuleFor(x => x.Email)
								.NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.Email))
								.MaximumLengthWithMessage(Constraints.C64, nameof(CreateAndAssignAuthorCommand.Email));

						RuleFor(x => x.FirstName)
								.NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.FirstName))
								.MaximumLengthWithMessage(Constraints.C64, nameof(CreateAndAssignAuthorCommand.FirstName));

						RuleFor(x => x.LastName)
								.NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.LastName))
								.MaximumLengthWithMessage(Constraints.C256, nameof(CreateAndAssignAuthorCommand.LastName));

						RuleFor(x => x.Title)
								.MaximumLengthWithMessage(Constraints.C32, nameof(CreateAndAssignAuthorCommand.LastName));

						RuleFor(x => x.Affiliation)
								.NotEmptyWithMessage(nameof(CreateAndAssignAuthorCommand.Affiliation))
								.MaximumLengthWithMessage(Constraints.C512, nameof(CreateAndAssignAuthorCommand.Affiliation));

				});

				RuleFor(x => x.ContributionAreas)
						.Must(HasMandatoryContribution)
						.WithMessage("The author must contribute to at least one mandatory area.");
		}

		private bool HasMandatoryContribution(HashSet<ContributionArea> contributionAreas)
				=> contributionAreas.Overlaps(ContributionAreaCategories.MandatoryAreas);
}
