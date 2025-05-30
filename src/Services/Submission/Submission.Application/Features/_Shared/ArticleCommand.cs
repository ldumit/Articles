﻿using System.Text.Json.Serialization;

namespace Submission.Application.Features.Shared;

public abstract record ArticleCommand<TActionType, TResponse> : IArticleAction<TActionType>, ICommand<TResponse>
		where TActionType : Enum
{
		[JsonIgnore]
		public int ArticleId { get; init; }

		public string? Comment { get; init; }

		[JsonIgnore]
		public abstract TActionType ActionType { get; }

		[JsonIgnore]
		public string Action => ActionType.ToString();

		[JsonIgnore]
		public DateTime CreatedOn => DateTime.UtcNow;

		[JsonIgnore]
		public int CreatedById { get; set; }
}

public abstract record ArticleCommand : ArticleCommand<ArticleActionType, IdResponse>;

public abstract class ArticleCommandValidator<TFileActionCommand> : AbstractValidator<TFileActionCommand>
    where TFileActionCommand : IArticleAction
{
    public ArticleCommandValidator()
		{
				RuleFor(c => c.ArticleId).GreaterThan(0).WithMessageForInvalidId(nameof(ArticleCommand.ArticleId));
    }		
}
