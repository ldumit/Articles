namespace Review.Domain.Reviewers.Events
{
		// todo - write a handler to send email to the user
		public record ReviewerAssigned(int AuthorId, int UserId, IArticleAction action) 
				: DomainEvent(action);
}
