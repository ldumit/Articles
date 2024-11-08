using FastEndpoints;
using Production.Persistence.Repositories;
using Production.Domain.Entities;
using Articles.Abstractions;

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
