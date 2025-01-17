using Articles.Abstractions.Events;
using Articles.Abstractions.Events.Dtos;
using MassTransit;
using Review.Domain.Events;

namespace Review.Application.Features.Articles.AcceptArticle;

public class PublishArticleAceeptedEventHandler(ArticleRepository _articleRepository, IPublishEndpoint _publishEndpoint)
        : INotificationHandler<ArticleAcceptedDomainEvent>
{
    public async Task Handle(ArticleAcceptedDomainEvent notification, CancellationToken ct)
    {
        var article = await _articleRepository.GetFullArticleByIdOrThrow(notification.Article.Id);

        var articleDto = article.Adapt<ArticleDto>();
        await _publishEndpoint.Publish(new ArticleSubmittedEvent(article.Adapt<ArticleDto>()), ct);
    }
}
