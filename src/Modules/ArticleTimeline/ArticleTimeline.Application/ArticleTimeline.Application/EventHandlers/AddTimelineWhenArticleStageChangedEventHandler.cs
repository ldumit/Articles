using ArticleTimeline.Application.VariableResolvers;
using ArticleTimeline.Domain.Enums;
using ArticleTimeline.Persistence.Repositories;
using Production.Domain.Events;
using Blocks.EntityFrameworkCore;

namespace ArticleTimeline.Application.EventHandlers;

public class AddTimelineWhenArticleStageChangedEventHandler(TransactionProvider transactionProvider, TimelineRepository timelineRepository, VariableResolverFactory variableResolverFactory)
		: AddTimelineEventHandler<ArticleStageChanged>(transactionProvider, timelineRepository, variableResolverFactory)
{
		protected override SourceType GetSourceType() => SourceType.StageTransition;
		protected override string GetSourceId(ArticleStageChanged eventModel) => $"{eventModel.CurrentStage}->{eventModel.NewStage}";
}
