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
