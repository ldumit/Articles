namespace Submission.Domain.Events;

//todo - create a handler which will send an integration event for the Articles service
//todo - remove DomainEvent suffix from all domain events ?!?
public record ArticleCreatedDomainEvent(IArticleAction action, int ArticleId, int JournalId, string Title, ArticleType Type, string ScopeStatement)
		: DomainEvent(action);