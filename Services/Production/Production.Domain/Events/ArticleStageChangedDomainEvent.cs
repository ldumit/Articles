using Articles.Abstractions;

namespace Production.Domain.Events;

public record ArticleStageChangedDomainEvent(IArticleAction action, ArticleStage PreviousStage, ArticleStage NewStage) 
    : DomainEvent(action.ArticleId, action.Action, action.CreatedById, action.Comment)
{
}