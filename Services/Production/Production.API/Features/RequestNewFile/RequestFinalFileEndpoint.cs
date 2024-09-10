using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features;

[Authorize(Roles = "POF")]
//talk about custom verbs
[HttpPut("articles/{articleId:int}/final-file:request")]
[Tags("Assets")]
public class RequestFinalFileEndpoint(IServiceProvider serviceProvider, AssetRepository _assetRepository) 
		: BaseEndpoint<RequestFinalFileCommand, FileActionResponse>(serviceProvider)
{
		//public override void Configure()
		//{
		//		Description(x => x.WithTags("Assets"));
		//}

		public async override Task HandleAsync(RequestFinalFileCommand command, CancellationToken cancellationToken)
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
