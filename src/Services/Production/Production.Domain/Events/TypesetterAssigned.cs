using Articles.Abstractions;
using Production.Domain.Enums;

namespace Production.Domain.Events;

public record TypesetterAssigned(IArticleAction<ArticleActionType> action, int TypesetterId, int TypesetterUserId) 
		: DomainEvent(action);
