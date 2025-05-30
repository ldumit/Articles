﻿using FastEndpoints;
using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Production.Persistence.Repositories;
using Production.Domain.Entities;

namespace Production.API.Features.Shared;

public abstract class BaseEndpoint<TCommand, TResponse> : Endpoint<TCommand, TResponse>
        where TCommand : IArticleAction
{
    protected readonly ArticleRepository _articleRepository;
		protected Article _article;

		public BaseEndpoint(ArticleRepository articleRepository) 
        => _articleRepository = articleRepository;

		protected virtual ArticleStage NextStage => _article.Stage;
}
