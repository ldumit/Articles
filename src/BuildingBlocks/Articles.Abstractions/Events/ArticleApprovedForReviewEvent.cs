using Articles.Abstractions.Events.Dtos;

namespace Articles.Abstractions.Events;

public record ArticleApprovedForReviewEvent(ArticleDto Article);
