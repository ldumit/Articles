using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features;

[Authorize(Roles = "POF")]
//talk about custom verbs
[HttpPut("articles/{articleId:int}/final-file:cancel-request")]
[Tags("Assets")]
public class CancelRequestFinalFileEndpoint(IServiceProvider serviceProvider, AssetRepository _assetRepository) 
		: BaseEndpoint<CancelRequestFinalFileCommand, RequestFileCommandResponse>(serviceProvider)
{
		//public override void Configure()
		//{
		//		Description(x => x.WithTags("Assets"));
		//}

		public async override Task HandleAsync(CancelRequestFinalFileCommand command, CancellationToken cancellationToken)
		{
				//var asset = await _assetRepository.GetByTypeAndNumber(command.ArticleId, command.AssetType);
				var article = await _articleRepository.GetWithAssets(command.ArticleId);

				foreach (var assetRequest in command.AssetRequests)
				{
						var asset = article.Assets.SingleOrDefault(a => a.TypeCode == assetRequest.AssetType);
						asset.SetStatus(AssetStatus.Requested, command);

						//todo - AddFileAction in the domain object?
						await AddFileAction(asset, asset.LatestFile, command);
				}
				await _assetRepository.SaveChangesAsync();

				await SendAsync(new RequestFileCommandResponse
				{
						
				});
		}
}
