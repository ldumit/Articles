using FastEndpoints;
using Production.Persistence.Repositories;
using Production.Domain.Entities;
using Articles.Abstractions;

namespace Production.API.Features.Shared;

public abstract class BaseEndpoint<TCommand, TResponse> : Endpoint<TCommand, TResponse>
        where TCommand : IArticleCommand
{
    protected readonly ArticleRepository _articleRepository;

    public BaseEndpoint(ArticleRepository articleRepository) 
        => _articleRepository = articleRepository;

    protected virtual ArticleStage GetNextStage(Article article) => article.CurrentStage.Stage;
}
