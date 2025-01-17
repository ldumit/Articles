namespace Review.Domain.Events
{
		// todo - write a handler to send email to the user
		public record ReviewerAssigned(IArticleAction<ArticleActionType> action, int AuthorId, int UserId) 
				: DomainEvent(action);
}
