using Articles.IntegrationEvents.Contracts.Articles.Dtos;

namespace Articles.IntegrationEvents.Contracts.Articles;

public record ArticleAcceptedEvent(ArticleDto Article);
