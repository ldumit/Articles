using Articles.Abstractions;
using Production.Domain.Enums;

namespace Production.Domain.Events
{
		public record TypesetterAssignedDomainEvent(IArticleAction<ArticleActionType> action, int TypesetterId, int TypesetterUserId) 
				: DomainEvent<ArticleActionType>(action.ArticleId, action.ActionType, action.UserId, action.Comment)
		{
    }
}
