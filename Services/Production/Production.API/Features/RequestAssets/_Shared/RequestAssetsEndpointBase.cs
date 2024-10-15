using Articles.Abstractions;
using Mapster;
using Production.API.Features.Shared;
using Production.Application.Dtos;
using Production.Application.StateMachines;
using Production.Domain.Entities;
using Production.Domain.Enums;
using Production.Persistence.Repositories;
using System;

namespace Production.API.Features.RequestFiles.Shared;

public class RequestAssetsEndpointBase<TCommand>(ArticleRepository _articleRepository, AssetRepository _assetRepository, AssetStateMachineFactory factory)
    : AssetBaseEndpoint<TCommand, RequestAssetsResponse>(factory)
    where TCommand : RequestMultipleAssetsCommand
{
    public async override Task HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdWithAssetsAsync(command.ArticleId);

        var assetsResponse = new List<Asset>();
        foreach (var assetRequest in command.AssetRequests)
        {
            var asset = article.Assets
                .SingleOrDefault(asset => asset.Type == assetRequest.AssetType && asset.Number == assetRequest.AssetNumber);
            
            if (asset is null)
								asset = await CreateAsset(command, article, assetRequest.AssetType, assetRequest.AssetNumber);
            
            CheckAndThrowStateTransition(asset, command.ActionType);
						asset.SetState(AssetState.Requested, command);
						assetsResponse.Add(asset);
				}
				await _assetRepository.SaveChangesAsync();

        var response = new RequestAssetsResponse()
        {
            Assets = assetsResponse.Select(a => a.Adapt<AssetMinimalDto>())
        };				

				await SendAsync(response);
    }

		protected async virtual Task<Asset> CreateAsset(IArticleAction<AssetActionType> action, Article article, Domain.Enums.AssetType assetType, byte assetNumber)
		{
				var assetTypeEntity = _assetRepository.GetAssetType(assetType);
				var asset = article.CreateAsset(assetTypeEntity, assetNumber);

        return asset;
        //return await _assetRepository.AddAsync(asset);        
		}
}
