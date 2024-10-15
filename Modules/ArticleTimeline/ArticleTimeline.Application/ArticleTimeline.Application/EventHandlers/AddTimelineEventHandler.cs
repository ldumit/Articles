using ArticleTimeline.Application.VariableResolvers;
using ArticleTimeline.Domain;
using ArticleTimeline.Domain.Enums;
using ArticleTimeline.Persistence.Repositories;
using System.Text.RegularExpressions;
using Articles.System.Extensions;
using Articles.System;
using Mapster;
using MediatR;
using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Articles.Abstractions;

namespace ArticleTimeline.Application.EventHandlers;

public abstract class AddTimelineEventHandler<TDomainEvent>(TransactionProvider _transactionProvider, TimelineRepository _timelineRepository, VariableResolverFactory _variableResolverFactory)
		//: IEventHandler<ArticleStageChangedDomainEvent>
		: INotificationHandler<TDomainEvent>
		where TDomainEvent : DomainEvent
{
		public async Task Handle(TDomainEvent eventModel, CancellationToken ct)
		{
				_timelineRepository.Context.Database
						.UseTransaction(await _transactionProvider.GetCurrentTransaction(ct));
				
				var sourceId = GetSourceId(eventModel);
				var template  = await _timelineRepository.GetTimelineTemplate(GetSourceType(), sourceId);

				var resolverModel = eventModel.Adapt<TimelineResolverModel>();
				var timeline = new Timeline()
				{
						ArticleId = resolverModel.Action.ArticleId,
						NewStage = resolverModel.NewStage ?? resolverModel.CurrentStage,
						CurrentStage = resolverModel.CurrentStage,
						SourceId = sourceId,
						SourceType = GetSourceType(),
						CreatedById = resolverModel.Action.CreatedById,
						CreatedOn = resolverModel.Action.CreatedOn,
						Title = ResolveVariables(template.TitleTemplate, resolverModel),
						Description = ResolveVariables(template.DescriptionTemplate, resolverModel)
				};
				await _timelineRepository.AddAsync(timeline);

				await _timelineRepository.SaveChangesAsync();
		}

		protected abstract SourceType GetSourceType();
		protected abstract string GetSourceId(TDomainEvent eventModel);

		protected string ResolveVariables(string descriptionTemplate, TimelineResolverModel resolverModel)
		{
				const string Pattern = @"<<(.*?)>>";
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
