namespace Review.Domain.Events;

public record ArticleAcceptedDomainEvent(Article Article, IArticleAction action)
		: DomainEvent(action);