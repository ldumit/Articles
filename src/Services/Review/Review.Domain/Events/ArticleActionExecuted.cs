namespace Review.Domain.Events;

public record ArticleActionExecuted(Article Article, IArticleAction action)
		: DomainEvent(action);