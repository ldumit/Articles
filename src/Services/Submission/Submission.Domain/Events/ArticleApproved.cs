namespace Submission.Domain.Events;

public record ArticleApproved(Article Article, IArticleAction action)
		: DomainEvent(action);