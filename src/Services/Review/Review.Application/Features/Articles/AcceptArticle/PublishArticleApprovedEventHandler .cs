using Articles.Abstractions.Events;
using Articles.Abstractions.Events.Dtos;
using MassTransit;
using Review.Domain.Events;

namespace Review.Application.Features.Articles.AcceptArticle;

//todo Publish is not a good name even if it means Publish integration event
public class PublishArticleAceeptedEventHandler(ArticleRepository _articleRepository, IPublishEndpoint _publishEndpoint)
        : INotificationHandler<ArticleAccepted>
{
    public async Task Handle(ArticleAccepted notification, CancellationToken ct)
    {
        var article = await _articleRepository.GetFullArticleByIdOrThrow(notification.Article.Id);

        var articleDto = article.Adapt<ArticleDto>();
        await _publishEndpoint.Publish(new ArticleSubmittedEvent(article.Adapt<ArticleDto>()), ct);
    }
}
