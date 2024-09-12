using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Articles.System;
using Articles.Abstractions;

using Production.Domain.Enums;
using System.Text.Json.Serialization;

namespace Production.API.Features.Shared;

public abstract record ArticleCommand<TResponse> : Domain.IArticleAction, IRequest<TResponse>
{
    public int ArticleId { get; set; }

    public string Comment { get; init; }

    [JsonIgnore]
    public abstract ActionType ActionType { get; }

    //todo check why the FromClaim doesn't work
    //[FromClaim(JwtRegisteredClaimNames.Sub)]
    //[JsonIgnore]
    int IArticleAction.UserId { get; set; }

    //ActionType Domain.IArticleAction.ActionType => ActionType.AssignTypesetter;
}

public abstract record ArticleCommand3<TResponse> : Domain.IArticleAction, IRequest<TResponse>
{
    /// <summary>
    /// The article Id
    /// </summary>
    [FromRoute]
    [Required]
    public int ArticleId { get; set; }

    //talk - explain why the members have to be implemented explicitly, so they will not apear in swagger
    // the alternative will be to use JsonIgnore attribute
    ActionType Domain.IArticleAction.ActionType => GetActionType();

    int IArticleAction.UserId { get; set; }

    string IArticleAction.Comment => GetActionComment();

    protected abstract string GetActionComment();
    protected abstract ActionType GetActionType();
}

public abstract record ArticleCommand3<TBody, TResponse> : ArticleCommand3<TResponse>
        where TBody : CommandBody
{
    [FromBody]
    public TBody Body { get; set; }

    protected override string GetActionComment() => Body?.Comment;
}


public record CommandBody
{
    /// <summary>
    /// Add comments, if any.
    /// </summary>
    public string Comment { get; set; }
}

public record ArticleCommandResponse(int ArticleId);

public abstract class ArticleCommandValidator<TFileActionCommand> : BaseValidator<TFileActionCommand>
    where TFileActionCommand : IArticleAction
{
    public ArticleCommandValidator()
    {
        RuleFor(command => command.ArticleId).GreaterThan(ValidatorsConstants.Id)
            .WithMessage(ValidatorsMessagesConstants.InvalidId.FormatWith("articleId"));
    }
}
