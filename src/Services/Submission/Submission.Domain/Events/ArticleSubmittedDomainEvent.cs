namespace Submission.Domain.Events;

public record ArticleSubmittedDomainEvent(IArticleAction action)
		: DomainEvent(action);