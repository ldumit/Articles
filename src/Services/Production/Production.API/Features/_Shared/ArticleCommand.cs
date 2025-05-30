﻿using FluentValidation;
using Blocks.Core;
using Articles.Abstractions;

using Production.Domain.Enums;
using System.Text.Json.Serialization;

namespace Production.API.Features.Shared;

public abstract record ArticleCommand<TActionType, TResponse> : IArticleAction<TActionType>
    where TActionType : Enum
{
    public int ArticleId { get; set; }

    public string Comment { get; init; }

    [JsonIgnore]
    public abstract TActionType ActionType { get; }

		[JsonIgnore]
		public string Action => ActionType.ToString();

		[JsonIgnore]
		public DateTime CreatedOn => DateTime.UtcNow;

		//todo check why the FromClaim doesn't work
		//[FromClaim(JwtRegisteredClaimNames.Sub)]
		[JsonIgnore]
		public int CreatedById { get; set; }

    //ActionType Domain.IArticleAction.ActionType => ActionType.AssignTypesetter;
}

public abstract record ArticleCommand : ArticleCommand<ArticleActionType, IdResponse>;
public abstract record ArticleCommand<TResponse> : ArticleCommand<ArticleActionType, TResponse>;
public abstract record AssetCommand<TResponse> : ArticleCommand<AssetActionType, TResponse>;

public abstract class ArticleCommandValidator<TFileActionCommand> : BaseValidator<TFileActionCommand>
    where TFileActionCommand : IArticleAction
{
    public ArticleCommandValidator()
    {
        RuleFor(command => command.ArticleId).GreaterThan(ValidatorsConstants.Id)
            .WithMessage(ValidatorsMessagesConstants.InvalidId.FormatWith("articleId"));
    }
}
