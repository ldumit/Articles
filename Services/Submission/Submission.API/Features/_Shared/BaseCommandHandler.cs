using Articles.AspNetCore;
using AutoMapper;
using MediatR;
using Submission.Persistence.Repositories;
using Submission.Domain.Entities;
using Submission.Domain.Enums;
using Articles.Abstractions;

namespace Submission.API.Features.Shared;

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
    protected virtual ArticleStage GetNextStage(Article article) => article.Stage;
    #region Methods

    protected void ChangeStage(Article article, TCommand command, Domain.Enums.AssetType? assetType = null)
    {
        //article.SetStage(GetNextStage(article), command.ActionType, command.ActionComment,  _claimsProvider.GetUserId(), assetType,command.DiscussionGroupType);
    }
    #endregion
}
