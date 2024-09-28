using Articles.AspNetCore;
using FastEndpoints;
using Production.Persistence.Repositories;
using Production.Domain.Entities;
using Production.Domain.Enums;
using Production.Domain;
using Articles.Abstractions;
using AutoMapper;
//using Articles.Abstractions;

namespace Production.API.Features.Shared;

public abstract class BaseEndpoint<TCommand, TResponse> : Endpoint<TCommand, TResponse>
        //where TCommand : ArticleCommand<TResponse>
        where TCommand : IArticleCommand
{
    protected readonly ArticleRepository _articleRepository;

    public BaseEndpoint(IServiceProvider serviceProvider)
    {
        _articleRepository = serviceProvider.GetRequiredService<ArticleRepository>();
    }

    //public abstract Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken);
    protected virtual ArticleStage GetNextStage(Article article) => article.CurrentStage.Stage;
    #region Methods


    #endregion
}
