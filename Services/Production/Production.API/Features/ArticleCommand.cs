using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Production.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Articles.System;

namespace Production.API.Features;

public interface IArticleCommand
{
    int ArticleId { get; set; }
}
public interface IFileActionCommand : IArticleCommand
{
}

public interface IFileActionResponse
{
}

public abstract record ArticleActionCommand<TResponse> : IArticleCommand, IRequest<TResponse>
{
    /// <summary>
    /// The article Id
    /// </summary>
    [FromRoute]
    [Required]
    public int ArticleId { get; set; }
    internal abstract FileActionType ActionType { get; }

    internal abstract string ActionComment { get; }
    internal abstract DiscussionType DiscussionGroupType { get; }
}
public abstract record ArticleActionCommand<TBody, TResponse> : ArticleActionCommand<TResponse>
    where TBody : CommandBody
{
    [FromBody]
    public TBody Body { get; set; }

    internal override string ActionComment => Body?.Comment;
    internal override DiscussionType DiscussionGroupType => Body.DiscussionType;
}

public abstract record FileActionCommand<TResponse> : ArticleActionCommand<TResponse>, IFileActionCommand, IRequest<TResponse>
    where TResponse : IFileActionResponse
{
    internal int FileId { get; set; }
}

public record CommandBody
{
    /// <summary>
    /// Add comments, if any.
    /// </summary>
    public string Comment { get; set; }
    public DiscussionType DiscussionType { get; set; }
}

public record ArticleCommandResponse(int ArticleId)
{
    //public int ArticleId { get; set; }
}


public abstract class ArticleCommandValidator<TFileActionCommand> : BaseValidator<TFileActionCommand>
    where TFileActionCommand : IArticleCommand
{
    public ArticleCommandValidator()
    {
        RuleFor(command => command.ArticleId).GreaterThan(ValidatorsConstants.Id)
            .WithMessage(ValidatorsMessagesConstants.InvalidId.FormatWith("articleId"));
    }
}
