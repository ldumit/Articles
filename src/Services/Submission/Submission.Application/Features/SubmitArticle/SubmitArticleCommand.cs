namespace Submission.Application.Features.SubmitArticle;

public record SubmitArticleCommand : ArticleCommand
{
		public override ArticleActionType ActionType => ArticleActionType.Submit;
}


public class SubmitArticleCommandValidator : ArticleCommandValidator<SubmitArticleCommand>;
