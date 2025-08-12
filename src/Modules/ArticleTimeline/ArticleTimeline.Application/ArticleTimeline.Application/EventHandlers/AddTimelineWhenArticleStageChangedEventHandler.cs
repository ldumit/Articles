using Articles.Abstractions;
using ArticleTimeline.Application.VariableResolvers;
using ArticleTimeline.Domain.Enums;
using ArticleTimeline.Persistence.Repositories;
using Blocks.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Production.Domain.Articles.Events;
using Production.Domain.Shared.Enums;

namespace ArticleTimeline.Application.EventHandlers;

public class AddTimelineWhenArticleStageChangedEventHandler(TransactionProvider transactionProvider, TimelineRepository timelineRepository, DbContext dbContext, VariableResolverFactory variableResolverFactory)
		: AddTimelineEventHandler<ArticleStageChanged<ArticleActionType>, IArticleAction<ArticleActionType>>(transactionProvider, timelineRepository, dbContext,variableResolverFactory)
{
		protected override SourceType GetSourceType() => SourceType.StageTransition;
		protected override string GetSourceId(ArticleStageChanged<ArticleActionType> eventModel) => $"{eventModel.CurrentStage}->{eventModel.NewStage}";
}
