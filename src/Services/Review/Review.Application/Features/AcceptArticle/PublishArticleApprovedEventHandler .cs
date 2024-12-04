using Articles.Abstractions.Events;
using Articles.Abstractions.Events.Dtos;
using MassTransit;
using Review.Domain.Events;

namespace Review.Application.Features.AcceptArticle;

public class PublishArticleAceeptedEventHandler(ArticleRepository _articleRepository, IPublishEndpoint _publishEndpoint) 
		: INotificationHandler<ArticleApprovedDomainEvent>
{
		public async Task Handle(ArticleApprovedDomainEvent notification, CancellationToken ct)
		{
				var article = await _articleRepository.GetFullArticleByIdOrThrow(notification.Article.Id);

				var articleDto = article.Adapt<ArticleDto>();
				await _publishEndpoint.Publish(new ArticleSubmittedEvent(article.Adapt<ArticleDto>()), ct);
		}
}
