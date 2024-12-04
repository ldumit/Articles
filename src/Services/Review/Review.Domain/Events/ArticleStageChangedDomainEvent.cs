namespace Review.Domain.Events;

public record ArticleStageChangedDomainEvent(IArticleAction action, ArticleStage CurrentStage, ArticleStage NewStage)
		: DomainEvent(action);