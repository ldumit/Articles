using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features;

[Authorize(Roles = "POF")]
[HttpPut("final-file:requestnew")]
public class RequestNewFinalFilesCommandHandler(IServiceProvider serviceProvider, AssetRepository _assetRepository) 
		: BaseEndpoint<RequestNewFinalFileCommand, FileActionResponse>(serviceProvider)
{
		public async override Task HandleAsync(RequestNewFinalFileCommand command, CancellationToken cancellationToken)
		{
				var asset = await _assetRepository.GetWithArticleAsync(command.AssetId);

				asset.Status = AssetStatus.Requested;
				asset.LasModifiedOn = DateTime.UtcNow;
				asset.LastModifiedById = _claimsProvider.GetUserId();

				await AddFileAction(asset, asset.LatestFile, command);
				await _assetRepository.SaveChangesAsync();

				await SendAsync(new FileActionResponse
				{
						AssetId = asset.Id,
						FileId = asset.LatestFileId,
						FileServerId = asset.LatestFile.FileServerId,
						Version = asset.LatestFile.Version
				});
		}
}
