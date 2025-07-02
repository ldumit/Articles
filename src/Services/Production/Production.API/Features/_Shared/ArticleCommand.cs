using FluentValidation;
using Blocks.Core;
using Production.Domain.Enums;

namespace Production.API.Features.Shared;

public abstract record ArticleCommand : ArticleCommandBase<ArticleActionType>, ICommand<IdResponse>;
public abstract record ArticleCommand<TResponse> : ArticleCommandBase<ArticleActionType>, ICommand<TResponse>;
public abstract record AssetCommand<TResponse> : ArticleCommandBase<AssetActionType>, ICommand<TResponse>;

public abstract class ArticleCommandValidator<TFileActionCommand> : BaseValidator<TFileActionCommand>
    where TFileActionCommand : IArticleAction
{
    public ArticleCommandValidator()
    {
        RuleFor(command => command.ArticleId).GreaterThan(ValidatorsConstants.Id)
            .WithMessage(ValidatorsMessagesConstants.InvalidId.FormatWith("articleId"));
    }
}
