﻿using ArticleTimeline.Application.VariableResolvers;
using ArticleTimeline.Domain.Enums;
using ArticleTimeline.Persistence.Repositories;
using Production.Domain.Events;
using Blocks.EntityFrameworkCore;
using Production.Domain.Enums;
using Articles.Abstractions;

namespace ArticleTimeline.Application.EventHandlers;

public class AddTimelineWhenActionExecutedEventHandler(TransactionProvider transactionProvider, TimelineRepository timelineRepository, VariableResolverFactory variableResolverFactory)
		: AddTimelineEventHandler<AssetActionExecuted, IArticleAction>(transactionProvider, timelineRepository, variableResolverFactory)
{
		protected override SourceType GetSourceType() => SourceType.ActionExecuted;
		protected override string GetSourceId(AssetActionExecuted eventModel) => $"{eventModel.action.Action}";
}
