using Articles.Abstractions;
using Production.Domain.Enums;

namespace Production.Domain.Events;

public record ArticleStageChangedDomainEvent(IArticleAction<ArticleActionType> action, ArticleStage PreviousStage, ArticleStage NewStage) 
    : DomainEvent<ArticleActionType>(action.ArticleId, action.ActionType, action.UserId, action.Comment)
{
}