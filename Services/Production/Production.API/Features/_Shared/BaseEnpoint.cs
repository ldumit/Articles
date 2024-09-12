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
        where TCommand : Domain.IArticleAction
{
    protected readonly AutoMapper.IMapper _mapper;
    protected readonly ArticleRepository _articleRepository;

    public BaseEndpoint(IServiceProvider serviceProvider)
    {
        _mapper = serviceProvider.GetRequiredService<AutoMapper.IMapper>();
        _articleRepository = serviceProvider.GetRequiredService<ArticleRepository>();
    }

    //public abstract Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken);
    protected virtual ArticleStage GetNextStage(Article article) => article.CurrentStage.Stage;
    #region Methods

    protected void ChangeStage(Article article, TCommand command, Domain.Enums.AssetType? assetType = null)
    {
        article.SetStage(GetNextStage(article), command);
    }

    protected async Task AddFileAction(Asset asset, Domain.Entities.File file, TCommand command)
    {
        //var fileAction = _mapper.Map<FileAction>(command);
        //file.FileActions.Add(fileAction);

        //file.AddDomainEvent(new ActionExecutedDomainEvent()
        //{
        //    ArticleId = asset.Article.Id,
        //    AssetType = asset.TypeId,
        //    File = file,
        //    NewAction = fileAction.TypeId,
        //    UserId = fileAction.UserId,
        //    PreviousStage = asset.Article.StageId,
        //    AssetNumber = asset.AssetNumber ?? 0,
        //    Comment = fileAction.Comment,
        //    DiscussionType = command.DiscussionGroupType
        //});
    }
    #endregion
}
