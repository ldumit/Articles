using Articles.Abstractions;
using Submission.Domain.Enums;

namespace Submission.Domain.Events
{
		public record TypesetterAssignedDomainEvent(IArticleAction<ArticleActionType> action, int TypesetterId, int TypesetterUserId) 
				: DomainEvent(action)
		{
    }
}
