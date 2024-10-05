using Microsoft.AspNetCore.Authorization;
using Production.Persistence.Repositories;
using Production.API.Features.Shared;
using FastEndpoints;
using Production.Domain.Enums;
using Mapster;
using Articles.Security;
using Articles.Abstractions;

namespace Production.API.Features.ApproveAssets.ApproveDraftAsset;

[Authorize(Roles = $"{Role.POF},{Role.AUT}")]
[HttpPut("articles/{articleId:int}/assets/draft/{assetId:int}:approve")]
//[HttpPut("articles/{articleId:int}/assets/draft/{assetId:int}/actions/approve")]
[Tags("Assets")]

public class ApproveDraftAssetEndpoint(ArticleRepository articleRepository, AssetRepository _assetRepository)
    : BaseEndpoint<ApproveDraftAssetCommand, AssetActionResponse>(articleRepository)
{
    public override async Task HandleAsync(ApproveDraftAssetCommand command, CancellationToken ct)
    {
        var asset = await _assetRepository.GetByIdAsync(command.ArticleId, command.AssetId);
        asset.SetState(AssetState.Approved, command);
        //asset.Article.SetStage(ArticleStage.FinalProduction, command);

        await _assetRepository.SaveChangesAsync();
        await SendAsync(asset.Adapt<AssetActionResponse>());
    }
}
