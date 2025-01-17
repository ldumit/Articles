using Review.Application.Features.Articles._Shared;

namespace Review.Application.Features.Articles.SubmitArticle;

public record SubmitArticleCommand : ArticleCommand
{
    public override ArticleActionType ActionType => ArticleActionType.Submit;
}


public class SubmitArticleCommandValidator : ArticleCommandValidator<SubmitArticleCommand>;
