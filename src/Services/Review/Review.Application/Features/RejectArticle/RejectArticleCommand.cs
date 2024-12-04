namespace Review.Application.Features.RejectArticle;

public record RejectArticleCommand : ArticleCommand
{
		public override ArticleActionType ActionType => ArticleActionType.Reject;
}


public class RejectArticleCommandValidator : ArticleCommandValidator<RejectArticleCommand>;
