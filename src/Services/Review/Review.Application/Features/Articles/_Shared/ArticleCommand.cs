﻿namespace Review.Application.Features.Articles._Shared;

public abstract record ArticleCommand : ArticleCommandBase<ArticleActionType>, Domain.IArticleAction, ICommand<IdResponse>;
public abstract record ArticleCommand<TResponse> : ArticleCommandBase<ArticleActionType>, Domain.IArticleAction, ICommand<TResponse>;

public abstract class ArticleCommandValidator<TFileActionCommand> : AbstractValidator<TFileActionCommand>
    where TFileActionCommand : IArticleAction
{
    public ArticleCommandValidator()
    {
        RuleFor(c => c.ArticleId).GreaterThan(0).WithMessageForInvalidId(nameof(ArticleCommand.ArticleId));
    }
}
