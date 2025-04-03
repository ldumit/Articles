namespace Submission.Domain.Events;

//todo - do I need this event?
public record AuthorCreated(Author author, IArticleAction action)
		: DomainEvent(action);
