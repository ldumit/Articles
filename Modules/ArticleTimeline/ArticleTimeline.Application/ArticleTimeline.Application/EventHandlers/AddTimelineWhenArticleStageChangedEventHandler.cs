using ArticleTimeline.Application.VariableResolvers;
using ArticleTimeline.Domain;
using ArticleTimeline.Domain.Enums;
using ArticleTimeline.Persistence.Repositories;
using FastEndpoints;
using Production.Domain.Events;
using System.Text.RegularExpressions;
using Articles.System.Extensions;
using Articles.System;

namespace ArticleTimeline.Application.EventHandlers;

internal class AddTimelineWhenArticleStageChangedEventHandler(TimelineRepository _timelineRepository, VariableResolverFactory _variableResolverFactory) 
		: IEventHandler<ArticleStageChangedDomainEvent>
{
		private const string Pattern = @"<<(.*?)>>";

		public async Task HandleAsync(ArticleStageChangedDomainEvent eventModel, CancellationToken ct)
		{
				var template  = await _timelineRepository.GetTimelineTemplate(SourceType.StageTransition, (int) eventModel.NewStage, eventModel.PreviousStage);

				var resolverModel = new TimelineResolverModel();
				var timeline = new Timeline()
				{
						ArticleId = eventModel.ArticleId,
						Stage = eventModel.PreviousStage,
						SourceId = (int)eventModel.NewStage,
						SourceType = SourceType.StageTransition,
						CreatedById = eventModel.UserId,
						Title = template.TitleTemplate,
						Description = ResolveDescription(template.DescriptionTemplate, resolverModel)
				};
				await _timelineRepository.AddAsync(timeline);

				await _timelineRepository.SaveChangesAsync();
		}

		public string ResolveDescription(string descriptionTemplate, TimelineResolverModel resolverModel)
		{
				var keys = descriptionTemplate.Matches(Pattern);
				foreach (Match key in keys.Cast<Match>())
				{
						var value = key.Value.Replace("<<", "").Replace(">>", "");
						var articleHistoryVariableType = value.ToEnum<VariableResolverType>();
						var resolvedValue = _variableResolverFactory(articleHistoryVariableType).GetValue(resolverModel).Result;
						descriptionTemplate = descriptionTemplate.Replace(key.Value, resolvedValue.ToString());
				}
				return descriptionTemplate;
		}
}
