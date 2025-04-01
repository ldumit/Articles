namespace Submission.Domain.Events;

//todo add a handler for this event who will call Auth service to create a user
public record AuthorCreated(Author author, IArticleAction action)
		: DomainEvent(action);
