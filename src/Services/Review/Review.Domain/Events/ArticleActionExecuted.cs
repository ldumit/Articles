﻿namespace Review.Domain.Events;

public record ArticleActionExecuted(IArticleAction<ArticleActionType> action, Article Article)
		: DomainEvent(action);