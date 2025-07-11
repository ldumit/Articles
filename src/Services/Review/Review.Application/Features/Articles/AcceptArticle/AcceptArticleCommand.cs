﻿using Review.Application.Features.Articles._Shared;

namespace Review.Application.Features.Articles.AcceptArticle;

public record AcceptArticleCommand : ArticleCommand
{
    public override ArticleActionType ActionType => ArticleActionType.AcceptArticle;
}


public class AcceptArticleCommandValidator : ArticleCommandValidator<AcceptArticleCommand>;
