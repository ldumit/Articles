namespace Production.Domain.Events
{
		public record TypesetterAssignedDomainEvent(IArticleAction action, int TypesetterId, int TypesetterUserId) 
				: DomainEvent(action.ArticleId, action.ActionType, action.UserId, action.Comment)
		{
    }
}
