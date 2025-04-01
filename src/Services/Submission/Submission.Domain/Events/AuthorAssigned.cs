namespace Submission.Domain.Events
{
		// todo - write a handler to send email to the user
		// todo - update all the events to use the Domain Entities instead of basic parameters
		public record AuthorAssigned(Author author, IArticleAction<ArticleActionType> action)
				: DomainEvent(action);
}
