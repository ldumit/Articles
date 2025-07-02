using Production.Domain.Enums;

namespace Production.Domain.Events;

public record TypesetterAssigned(int TypesetterId, int TypesetterUserId, IArticleAction action) 
		: DomainEvent(action);
