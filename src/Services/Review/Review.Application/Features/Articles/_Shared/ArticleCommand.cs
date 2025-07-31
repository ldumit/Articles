using Review.Domain.Shared;
using Review.Domain.Articles.Enums;

namespace Review.Application.Features.Articles._Shared;

public abstract record ArticleCommand : ArticleCommandBase<ArticleActionType>, IArticleAction, ICommand<IdResponse>;
public abstract record ArticleCommand<TResponse> : ArticleCommandBase<ArticleActionType>, IArticleAction, ICommand<TResponse>;

public abstract class ArticleCommandValidator<TFileActionCommand> : AbstractValidator<TFileActionCommand>
    where TFileActionCommand : IArticleAction
{
    public ArticleCommandValidator()
    {
        RuleFor(c => c.ArticleId).GreaterThan(0).WithMessageForInvalidId(nameof(ArticleCommand.ArticleId));
    }
}
