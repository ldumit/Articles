using Articles.Abstractions;

namespace Production.Domain.Events;

public record ArticleStageChangedDomainEvent(IArticleAction action, ArticleStage PreviousStage, ArticleStage NewStage) 
    : DomainEvent(action.ArticleId, action.ActionType, action.UserId, action.ActionComment)
{
}