using Microsoft.AspNetCore.Authorization;
using Production.Persistence.Repositories;
using Production.API.Features.Shared;
using FastEndpoints;
using Production.Domain.Enums;
using Mapster;
using Articles.Security;
using Articles.Abstractions;
using Production.Application.Dtos;
using Production.Application.StateMachines;

namespace Production.API.Features.ApproveAssets.ApproveDraftAsset;

[Authorize(Roles = $"{Role.POF},{Role.CORAUT}")]
[HttpPut("articles/{articleId:int}/assets/draft/{assetId:int}:approve")]
//[HttpPut("articles/{articleId:int}/assets/draft/{assetId:int}/actions/approve")]
[Tags("Assets")]

public class ApproveDraftAssetEndpoint(AssetRepository assetRepository, AssetStateMachineFactory stateMachineFactory)
    : AssetBaseEndpoint<ApproveDraftAssetCommand, AssetActionResponse>(assetRepository, stateMachineFactory)
{
    public override async Task HandleAsync(ApproveDraftAssetCommand command, CancellationToken ct)
    {
        var asset = await _assetRepository.GetByIdAsync(command.ArticleId, command.AssetId);
        _article = asset!.Article;
        CheckAndThrowStateTransition(asset!, command.ActionType);

				asset.SetState(AssetState.Approved, command);
				_article.SetStage(NextStage, command);

        await _assetRepository.SaveChangesAsync();
        await SendAsync(new AssetActionResponse(asset.Adapt<AssetMinimalDto>()));
    }

		protected override ArticleStage NextStage => ArticleStage.FinalProduction;
}
