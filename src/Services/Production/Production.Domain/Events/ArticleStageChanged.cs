using Articles.Abstractions;
using Articles.Abstractions.Enums;

namespace Production.Domain.Events;

public record ArticleStageChanged(IArticleAction action, ArticleStage CurrentStage, ArticleStage NewStage)
		: DomainEvent(action);