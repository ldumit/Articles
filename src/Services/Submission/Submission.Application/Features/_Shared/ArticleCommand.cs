using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Articles.System;
using Articles.Abstractions;
using Submission.Domain.Enums;
using Articles.MediatR;

namespace Submission.Application.Features.Shared;

public abstract record ArticleCommand<TActionType, TResponse> : IArticleAction<TActionType>, ICommand<TResponse>
    where TActionType : Enum
{
    [FromRoute]
    public int ArticleId { get; set; }

    public string? Comment { get; init; }

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
}

public abstract record ArticleCommand : ArticleCommand<ArticleActionType, IdResponse>;

public abstract class ArticleCommandValidator<TFileActionCommand> : BaseValidator<TFileActionCommand>
    where TFileActionCommand : IArticleAction
{
    public ArticleCommandValidator()
    {
        RuleFor(command => command.ArticleId).GreaterThan(ValidatorsConstants.Id)
            .WithMessage(ValidatorsMessagesConstants.InvalidId.FormatWith("articleId"));
    }
}
