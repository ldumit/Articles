using Articles.Abstractions;
using Production.Domain.Enums;

namespace Production.Domain.Events;

public record ArticleStageChangedDomainEvent(IArticleAction action, ArticleStage PreviousStage, ArticleStage NewStage) 
    : DomainEvent(action.ArticleId, action.Action, action.CreatedById, action.Comment)
{
}