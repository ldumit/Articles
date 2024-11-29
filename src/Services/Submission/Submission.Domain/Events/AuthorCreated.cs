namespace Submission.Domain.Events;

//todo add a handler for this event who will call Auth service to create a user
public record AuthorCreated(string Email, string FirstName, string LastName, string? Title, string Affiliation, IArticleAction action)
		: DomainEvent(action);
