using Articles.Abstractions.Events.Dtos;

namespace Articles.Abstractions.Events;

public record ArticleAcceptedEvent(ArticleDto Article);
