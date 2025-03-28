namespace Review.Domain.Events;

public record ArticleStageChanged(IArticleAction action, ArticleStage CurrentStage, ArticleStage NewStage)
		: DomainEvent(action);