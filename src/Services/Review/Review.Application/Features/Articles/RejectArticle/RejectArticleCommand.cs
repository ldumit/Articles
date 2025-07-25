﻿using Review.Application.Features.Articles._Shared;

namespace Review.Application.Features.Articles.RejectArticle;

public record RejectArticleCommand : ArticleCommand
{
    public override ArticleActionType ActionType => ArticleActionType.RejectArticle;
}


public class RejectArticleCommandValidator : ArticleCommandValidator<RejectArticleCommand>;
