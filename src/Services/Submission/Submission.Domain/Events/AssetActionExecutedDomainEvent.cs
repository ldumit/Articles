using Articles.Abstractions;
using Submission.Domain.Enums;

namespace Submission.Domain.Events;

public record AssetActionExecutedDomainEvent(IArticleAction<AssetActionType> action, ArticleStage CurrentStage, AssetType AssetType, int AssetNumber, Entities.File? File)
		: DomainEvent(action);