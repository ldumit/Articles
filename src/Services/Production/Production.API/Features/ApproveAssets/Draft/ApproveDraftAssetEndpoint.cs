using Mapster;
using Articles.Abstractions.Enums;
using Production.Persistence.Repositories;
using Production.API.Features.Shared;
using Production.Domain.Enums;
using Production.Application.Dtos;
using Production.Application.StateMachines;

namespace Production.API.Features.ApproveAssets.ApproveDraftAsset;

[Authorize(Roles = $"{Role.POF},{Role.CORAUT}")]
[HttpPut("articles/{articleId:int}/assets/draft/{assetId:int}:approve")]
[Tags("Assets")]

public class ApproveDraftAssetEndpoint(AssetRepository _assetRepository, AssetTypeRepository assetTypeRepository, AssetStateMachineFactory stateMachineFactory)
    : AssetBaseEndpoint<ApproveDraftAssetCommand, AssetActionResponse>(assetTypeRepository, stateMachineFactory)
{
    public override async Task HandleAsync(ApproveDraftAssetCommand command, CancellationToken ct)
    {
        var asset = await _assetRepository.GetByIdAsync(command.ArticleId, command.AssetId);
        _article = asset.Article;

				//stateMachineFactory.ValidateStageTransition(_article.Stage, NextStage, asset.Type, command.ActionType);

				CheckAndThrowStateTransition(asset, command.ActionType);
				asset.SetState(AssetState.Approved, command);

				_article.SetStage(NextStage, command);

        await _assetRepository.SaveChangesAsync();
        await SendAsync(new AssetActionResponse(asset.Adapt<AssetMinimalDto>()));
    }

		protected override ArticleStage NextStage => ArticleStage.FinalProduction;
}
