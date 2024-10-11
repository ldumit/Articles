using ArticleTimeline.Application.VariableResolvers;
using ArticleTimeline.Domain;
using ArticleTimeline.Domain.Enums;
using ArticleTimeline.Persistence.Repositories;
using FastEndpoints;
using Production.Domain.Events;
using System.Text.RegularExpressions;
using Articles.System.Extensions;
using Articles.System;
using Microsoft.Extensions.DependencyInjection;
using Mapster;

namespace ArticleTimeline.Application.EventHandlers;

public class AddTimelineWhenTestDomainEvent : IEventHandler<TestDomainEvent>
{
		public async Task HandleAsync(TestDomainEvent eventModel, CancellationToken ct)
		{
				Console.WriteLine(eventModel.ToString());
		}
}

public class AddTimelineWhenArticleStageChangedEventHandler(IServiceScopeFactory _scopeFactory)
		: IEventHandler<ArticleStageChangedDomainEvent>
{
		private const string Pattern = @"<<(.*?)>>";

		public async Task HandleAsync(ArticleStageChangedDomainEvent eventModel, CancellationToken ct)
		{
				using var scope = _scopeFactory.CreateScope(); // FastEnpoints event handlers are singletons therefore the ctor wwill be only once called
				var timelineRepository = scope.Resolve<TimelineRepository>();

				var sourceId = $"{ eventModel.PreviousStage}->{eventModel.NewStage}";

				var template  = await timelineRepository.GetTimelineTemplate(SourceType.StageTransition, sourceId);

				var resolverModel = eventModel.Adapt<TimelineResolverModel>();
				var timeline = new Timeline()
				{
						ArticleId = eventModel.ArticleId,
						NextStage = eventModel.NewStage,
						PreviousStage = eventModel.PreviousStage,
						SourceId = sourceId,
						SourceType = SourceType.StageTransition,
						CreatedById = eventModel.UserId,
						Title = ResolveVariables(scope, template.TitleTemplate, resolverModel),
						Description = ResolveVariables(scope, template.DescriptionTemplate, resolverModel)
				};
				await timelineRepository.AddAsync(timeline);

				await timelineRepository.SaveChangesAsync();
		}

		public string ResolveVariables(IServiceScope scope, string descriptionTemplate, TimelineResolverModel resolverModel)
		{
				var variableResolverFactory = scope.Resolve<VariableResolverFactory>();
				var keys = descriptionTemplate.Matches(Pattern);
				foreach (Match key in keys.Cast<Match>())
				{
						var value = key.Value.Replace("<<", "").Replace(">>", "");
						var articleHistoryVariableType = value.ToEnum<VariableResolverType>();
						var resolvedValue = variableResolverFactory(articleHistoryVariableType).GetValue(resolverModel).Result;
						descriptionTemplate = descriptionTemplate.Replace(key.Value, resolvedValue.ToString());
				}
				return descriptionTemplate;
		}
}
