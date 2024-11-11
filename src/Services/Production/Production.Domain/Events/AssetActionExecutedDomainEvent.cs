using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Production.Domain.Enums;

namespace Production.Domain.Events;

public record AssetActionExecutedDomainEvent(IArticleAction<AssetActionType> action, ArticleStage CurrentStage, AssetType AssetType, int AssetNumber, Entities.File? File)
		: DomainEvent(action);