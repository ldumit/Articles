namespace Review.Domain.Events;

public record ReviewerCreated(Reviewer author, IArticleAction action) 
		: DomainEvent(action);

