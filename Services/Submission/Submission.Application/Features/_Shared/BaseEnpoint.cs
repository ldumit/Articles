using FastEndpoints;
using Submission.Persistence.Repositories;
using Submission.Domain.Entities;
using Articles.Abstractions;

namespace Submission.Application.Features.Shared;

public abstract class BaseEndpoint<TCommand, TResponse> : Endpoint<TCommand, TResponse>
        where TCommand : IArticleAction
{
    protected readonly ArticleRepository _articleRepository;
		protected Article _article;

		public BaseEndpoint(ArticleRepository articleRepository) 
        => _articleRepository = articleRepository;

		protected virtual ArticleStage NextStage => _article.Stage;
}
