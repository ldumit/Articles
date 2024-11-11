using Articles.Abstractions;
using Articles.Abstractions.Enums;

namespace Submission.Domain.Events;

public record ArticleStageChangedDomainEvent(IArticleAction action, ArticleStage CurrentStage, ArticleStage NewStage)
		: DomainEvent(action);