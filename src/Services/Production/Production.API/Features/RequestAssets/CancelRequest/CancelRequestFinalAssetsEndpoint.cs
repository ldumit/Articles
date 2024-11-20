using Articles.Security;
using FastEndpoints;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Production.API.Features.RequestFiles.Shared;
using Production.API.Features.Shared;
using Production.Application.Dtos;
using Production.Domain.Entities;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features.RequestFiles.Cancel;

[Authorize(Roles = Role.POF)]
[HttpPut("articles/{articleId:int}/assets/final:cancel-request")]
[Tags("Assets")]
public class CancelRequestFinalAssetsEndpoint(ArticleRepository articleRepository, AssetRepository _assetRepository)
    : BaseEndpoint<CancelRequestFinalAssetsCommand, RequestAssetsResponse>(articleRepository)
{
    public async override Task HandleAsync(CancelRequestFinalAssetsCommand command, CancellationToken cancellationToken)
    {
        _article = await _articleRepository.GetByIdWithAssetsAsync(command.ArticleId);

				var assets = new List<Asset>();
				foreach (var assetRequest in command.AssetRequests)
        {
						var asset = _article.Assets
            				.SingleOrDefault(asset => asset.Type == assetRequest.AssetType && asset.Number == assetRequest.AssetNumber);

            if (asset?.State == AssetState.Requested)
                continue;
            
            asset!.SetState(AssetState.Uploaded, command);
						assets.Add(asset);
				}
				
        _article.SetStage(NextStage, command);
				await _assetRepository.SaveChangesAsync();

        await SendAsync(new RequestAssetsResponse
        {
						Assets = assets.Select(a => a.Adapt<AssetMinimalDto>())
				});
    }
}
