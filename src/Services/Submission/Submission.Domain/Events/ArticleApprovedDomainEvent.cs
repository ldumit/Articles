namespace Submission.Domain.Events;

public record ArticleApprovedDomainEvent(Article Article, IArticleAction action)
		: DomainEvent(action);