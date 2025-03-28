namespace Review.Domain.Events;

//todo - create a handler which will send an integration event for the Articles service
//todo - implement all domain event handlers or delete the domain event if not needed
public record ArticleCreated(IArticleAction action, int ArticleId, int JournalId, string Title, ArticleType Type, string ScopeStatement)
		: DomainEvent(action);