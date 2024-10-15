using Articles.Abstractions;
using Articles.Domain;

namespace Production.Domain.Events;

public record TestDomainEvent(string Value): IDomainEvent;

//public record ArticleStageChangedDomainEvent(IArticleAction action, ArticleStage PreviousStage, ArticleStage NewStage)
//		: IDomainEvent
//{
//}

public record ArticleStageChangedDomainEvent(IArticleAction action, ArticleStage CurrentStage, ArticleStage NewStage)
		: DomainEvent(action)
{
}