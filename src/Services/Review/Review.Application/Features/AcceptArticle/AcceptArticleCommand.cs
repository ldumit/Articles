namespace Review.Application.Features.AcceptArticle;

public record AcceptArticleCommand : ArticleCommand
{
		public override ArticleActionType ActionType => ArticleActionType.Approve;
}


public class AcceptArticleCommandValidator : ArticleCommandValidator<AcceptArticleCommand>;
