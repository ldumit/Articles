using Articles.AspNetCore;
using MediatR;
using Submission.Persistence.Repositories;
using Submission.Domain.Entities;
using Articles.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Submission.Application.Features.Shared;

public abstract class BaseCommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ArticleCommand<TResponse>
{
    protected readonly ClaimsProvider _claimsProvider;
    protected IMediator _mediator;

    protected readonly ArticleRepository _articleRepository;


    public BaseCommandHandler(IServiceProvider serviceProvider)
    {
        _claimsProvider = serviceProvider.GetRequiredService<ClaimsProvider>();
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
