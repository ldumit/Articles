using Microsoft.AspNetCore.Authorization;
using Production.API.Features.ApproveDraftPdf;
using Production.Persistence.Repositories;
using Production.API.Features.Shared;
using Production.API.Features.UploadFiles.Shared;
using FastEndpoints;
using Production.Domain.Enums;
using Mapster;

namespace Production.API.Features.RequestFiles.AuthorFiles;

[Authorize(Roles = "POF,AUT")]
[HttpPut("articles/{articleId:int}/draft-pdf/{assetId:int}:approve")]
[Tags("Assets")]

public class ApproveDraftPdfEndpoint(IServiceProvider serviceProvider, AssetRepository _assetRepository)
				: BaseEndpoint<ApproveDraftPdfCommand, AssetResponse>(serviceProvider)
{
		public override async Task HandleAsync(ApproveDraftPdfCommand command, CancellationToken ct)
		{
				var asset = await _assetRepository.GetByIdAsync(command.ArticleId, command.AssetId);
				asset.SetStatus(AssetState.Approved, command);
				//asset.Article.SetStage(GetNextStage(asset.Article), command);

				await _assetRepository.SaveChangesAsync();
				await SendAsync(asset.Adapt<AssetResponse>());
				//await SendAsync(new AssetResponse(asset.Id, asset.CurrentFile.Id, asset.CurrentFile.Version, asset.CurrentFile.FileServerId));
		}
}
