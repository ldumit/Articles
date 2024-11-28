using ArticleHub.Domain;
using ArticleHub.Persistence;
using Articles.Abstractions.Events;
using Mapster;
using MassTransit;

namespace ArticleHub.Application.Features.Articles.EventHandlers;

public class ArticleSubmittedEventHandler(ArticleHubDbContext _dbContext) : IConsumer<ArticleSubmittedEvent>
{
		public async Task Consume(ConsumeContext<ArticleSubmittedEvent> context)
		{
				var article = context.Message.Article.Adapt<Article>();
				_dbContext.Articles.Add(article);
				await _dbContext.SaveChangesAsync();
		}
}
