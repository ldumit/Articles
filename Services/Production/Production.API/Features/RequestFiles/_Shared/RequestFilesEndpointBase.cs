using Production.API.Features.Shared;
using Production.API.Features.UploadFiles.Shared;
using Production.Domain;
using Production.Domain.Entities;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features.RequestFiles.Shared;

public class RequestFilesEndpointBase<TCommand>(IServiceProvider serviceProvider, AssetRepository _assetRepository)
        : BaseEndpoint<TCommand, RequestFilesCommandResponse>(serviceProvider)
        where TCommand : RequestMultipleFilesCommand
{
    public async override Task HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdWithAssetsAsync(command.ArticleId);
        var response = new RequestFilesCommandResponse();
        foreach (var assetRequest in command.AssetRequests)
        {
            var asset = article.Assets
                    .SingleOrDefault(asset => asset.TypeCode == assetRequest.AssetType && asset.AssetNumber == assetRequest.AssetNumber);
            if (asset != null)
            {
                asset.SetStatus(AssetStatus.Requested, command);
								response.Assets.Add(
		                _mapper.Map<FileResponse>(asset.CurrentFile));
						}
            else
            {
								asset = CreateAsset(command, assetRequest.AssetType, assetRequest.AssetNumber);
								response.Assets.Add(
		                new FileResponse(asset.Id, asset.CurrentFile?.Id, asset.CurrentFile?.Version, asset.CurrentFile?.FileServerId));
						}


				}
				await _assetRepository.SaveChangesAsync();

        await SendAsync(response);
    }

		protected virtual Asset CreateAsset(IArticleAction action, Domain.Enums.AssetType assetType, byte assetNumber)
		{
				var assetTypeEntity = _assetRepository.GetAssetType(assetType);

				return Asset.CreateFromRequest(action, assetTypeEntity, assetNumber);
		}
}
