namespace Review.Domain.Articles.Events;


public record ReviewReportUploaded(Asset asset, IArticleAction action)
		: DomainEvent(action);
