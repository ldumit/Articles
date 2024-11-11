using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Submission.Domain.Entities;
using Submission.Domain.Enums;

namespace Submission.Domain.Events;

public record ArticleActionExecuted(IArticleAction<ArticleActionType> action, Article Article)
		: DomainEvent(action);