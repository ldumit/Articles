namespace Submission.Domain.Events;

//todo - create a handler which will send an integration event for the ArticleHub service
//todo - remove DomainEvent suffix from all domain events ?!?
public record ArticleCreated(Article Article, IArticleAction action)
		: DomainEvent(action);