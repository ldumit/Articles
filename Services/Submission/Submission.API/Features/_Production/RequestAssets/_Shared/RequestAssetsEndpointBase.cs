using Mapster;
using Submission.API.Features.Shared;
using Submission.Application.Dtos;
using Submission.Application.StateMachines;
using Submission.Domain.Entities;
using Submission.Domain.Enums;
using Submission.Persistence.Repositories;

namespace Submission.API.Features.RequestFiles.Shared;

public class RequestAssetsEndpointBase<TCommand>(ArticleRepository _articleRepository, AssetRepository assetRepository, AssetStateMachineFactory factory)
    : AssetBaseEndpoint<TCommand, RequestAssetsResponse>(assetRepository, factory)
    where TCommand : RequestMultipleAssetsCommand
{
    public async override Task HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        _article = await _articleRepository.GetByIdWithAssetsAsync(command.ArticleId);

        var assetsResponse = new List<Asset>();
        foreach (var assetRequest in command.AssetRequests)
        {
            var asset = _article.Assets
                .SingleOrDefault(asset => asset.Type == assetRequest.AssetType && asset.Number == assetRequest.AssetNumber);
            
            if (asset is null)
								asset = CreateAsset(assetRequest.AssetType, assetRequest.AssetNumber);

						CheckAndThrowStateTransition(asset, command.ActionType);
						asset.SetState(AssetState.Requested, command);
						
            assetsResponse.Add(asset);
				}

				_article.SetStage(NextStage, command);
				await _articleRepository.SaveChangesAsync();

        var response = new RequestAssetsResponse()
        {
            Assets = assetsResponse.Select(a => a.Adapt<AssetMinimalDto>())
        };				
				await SendAsync(response);
    }
}
