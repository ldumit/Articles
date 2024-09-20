using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Articles.System;
using Articles.Abstractions;

using Production.Domain.Enums;
using System.Text.Json.Serialization;

namespace Production.API.Features.Shared;

public abstract record ArticleCommand<TActionType, TResponse> : IArticleAction<TActionType>, IRequest<TResponse>
    where TActionType : Enum
{
    public int ArticleId { get; set; }

    public string Comment { get; init; }

    [JsonIgnore]
    public abstract TActionType ActionType { get; }

    //todo check why the FromClaim doesn't work
    //[FromClaim(JwtRegisteredClaimNames.Sub)]
    //[JsonIgnore]
    int IArticleCommand.UserId { get; set; }

    //ActionType Domain.IArticleAction.ActionType => ActionType.AssignTypesetter;
}

public abstract record ArticleCommand : ArticleCommand<ArticleActionType, ArticleCommandResponse>;
public abstract record ArticleCommand<TResponse> : ArticleCommand<ArticleActionType, TResponse>;
public abstract record AssetCommand<TResponse> : ArticleCommand<AssetActionType, TResponse>;

public record ArticleCommandResponse(int ArticleId);

public abstract class ArticleCommandValidator<TFileActionCommand> : BaseValidator<TFileActionCommand>
    where TFileActionCommand : IArticleCommand
{
    public ArticleCommandValidator()
    {
        RuleFor(command => command.ArticleId).GreaterThan(ValidatorsConstants.Id)
            .WithMessage(ValidatorsMessagesConstants.InvalidId.FormatWith("articleId"));
    }
}
