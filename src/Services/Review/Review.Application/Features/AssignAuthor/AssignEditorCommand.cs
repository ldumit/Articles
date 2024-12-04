namespace Review.Application.Features.AssignEditor;

public record AssignEditorCommand(int EditorId)
		: ArticleCommand
{
		public override ArticleActionType ActionType => ArticleActionType.AssignEditor;
}

public class AssignEditorCommandValidator : ArticleCommandValidator<AssignEditorCommand>
{
		public AssignEditorCommandValidator()
		{
				RuleFor(c => c.EditorId).GreaterThan(0).WithMessageForInvalidId(nameof(AssignEditorCommand.EditorId));
		}

		private bool HasMandatoryContribution(HashSet<ContributionArea> contributionAreas)
				=> contributionAreas.Overlaps(ContributionAreaCategories.MandatoryAreas);
}