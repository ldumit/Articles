namespace Submission.Domain.Events;

//todo - do I need to notify the ArticleHub, or it is enough only when the article is submitted?
public record ArticleCreated(Article Article, IArticleAction action)
		: DomainEvent(action);