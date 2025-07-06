using MassTransit;
using Articles.Abstractions.Events;
using Articles.Abstractions.Events.Dtos;
using Submission.Domain.Events;
using Blocks.Core;

namespace Submission.Application.Features.ApproveArticle;

public class PublishArticleApprovedEventHandler(ArticleRepository _articleRepository, IPublishEndpoint _publishEndpoint) 
		: INotificationHandler<ArticleApproved>
{
		public async Task Handle(ArticleApproved notification, CancellationToken ct)
		{
				var article = Guard.NotFound(await _articleRepository.GetFullArticleById(notification.Article.Id));

				var articleDto = article.Adapt<ArticleDto>(); 

				await _publishEndpoint.Publish(new ArticleSubmittedEvent(articleDto), ct);
		}
}
