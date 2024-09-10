using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features;

[Authorize(Roles = "TSOF")]
//talk about custom verbs
[HttpPut("articles/{articleId:int}/author-file:request")]
[Tags("Assets")]
public class RequestAuthorFileEndpoint(IServiceProvider serviceProvider, AssetRepository _assetRepository) 
		: BaseEndpoint<RequestAuthorFileCommand , FileActionResponse>(serviceProvider)
{

		public async override Task HandleAsync(RequestAuthorFileCommand command, CancellationToken cancellationToken)
		{
				var asset = await _assetRepository.GetWithArticleAsync(command.AssetId);

				asset.SetStatus(AssetStatus.Requested, command);

				//todo - AddFileAction in the domain object?
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
