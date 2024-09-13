using Articles.AspNetCore;
using AutoMapper;
using MediatR;
using Production.Persistence.Repositories;
using Production.Domain.Entities;
using Production.Domain.Enums;
using Articles.Abstractions;

namespace Production.API.Features.Shared;

public abstract class BaseCommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ArticleCommand<TResponse>
{
    protected readonly ClaimsProvider _claimsProvider;
    protected readonly IMapper _mapper;
    protected IMediator _mediator;

    protected readonly ArticleRepository _articleRepository;


    public BaseCommandHandler(IServiceProvider serviceProvider)
    {
        _claimsProvider = serviceProvider.GetRequiredService<ClaimsProvider>();
        _mapper = serviceProvider.GetRequiredService<IMapper>();
        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _articleRepository = serviceProvider.GetRequiredService<ArticleRepository>();
    }

    public abstract Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken);
    protected virtual ArticleStage GetNextStage(Article article) => article.CurrentStage.Stage;
    #region Methods

    protected void ChangeStage(Article article, TCommand command, Domain.Enums.AssetType? assetType = null)
    {
        //article.SetStage(GetNextStage(article), command.ActionType, command.ActionComment,  _claimsProvider.GetUserId(), assetType,command.DiscussionGroupType);
    }

    protected async Task AddFileAction(Asset asset, Domain.Entities.File file, TCommand command)
    {
        var fileAction = _mapper.Map<FileAction>(command);
        file.FileActions.Add(fileAction);

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
