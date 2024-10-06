using Articles.Abstractions;
using Mapster;
using Production.API.Features.Shared;
using Production.Application.Dtos;
using Production.Domain.Entities;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features.RequestFiles.Shared;

public class RequestAssetsEndpointBase<TCommand>(ArticleRepository articleRepository, AssetRepository _assetRepository)
        : BaseEndpoint<TCommand, RequestAssetsResponse>(articleRepository)
        where TCommand : RequestMultipleAssetsCommand
{
    public async override Task HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdWithAssetsAsync(command.ArticleId);

        var assets = new List<Asset>();
        foreach (var assetRequest in command.AssetRequests)
        {
            var asset = article.Assets
                    .SingleOrDefault(asset => asset.Type == assetRequest.AssetType && asset.Number == assetRequest.AssetNumber);
            if (asset != null)
                asset.SetState(AssetState.Requested, command);
            else
								asset = await CreateAsset(command, assetRequest.AssetType, assetRequest.AssetNumber);

            assets.Add(asset);
				}
				await _assetRepository.SaveChangesAsync();

        var response = new RequestAssetsResponse()
        {
            Assets = assets.Select(a => a.Adapt<AssetMinimalDto>())
        };				

				await SendAsync(response);
    }

		protected async virtual Task<Asset> CreateAsset(IArticleAction<AssetActionType> action, Domain.Enums.AssetType assetType, byte assetNumber)
		{
				var assetTypeEntity = _assetRepository.GetAssetType(assetType);
				var asset = Asset.CreateFromRequest(action, assetTypeEntity, assetNumber);

        return await _assetRepository.AddAsync(asset);        
		}
}
