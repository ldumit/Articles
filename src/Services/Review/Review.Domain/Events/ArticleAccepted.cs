namespace Review.Domain.Events;

public record ArticleAccepted(Article Article, IArticleAction action)
		: DomainEvent(action);