using Articles.Abstractions;
using Submission.Domain.Enums;

namespace Submission.Domain.Events
{
		// todo - write a handler to send email to the user
		public record AuthorAssigned(IArticleAction<ArticleActionType> action, int AuthorId, int UserId) 
				: DomainEvent(action);
}
