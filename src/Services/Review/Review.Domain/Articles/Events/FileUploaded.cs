namespace Review.Domain.Articles.Events;


public record FileUploaded(Asset asset, IArticleAction action)
		: DomainEvent(action);
