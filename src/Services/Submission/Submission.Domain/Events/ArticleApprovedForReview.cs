namespace Submission.Domain.Events;

public record ArticleApprovedForReview(Article Article, IArticleAction Action)
		: DomainEvent(Action);