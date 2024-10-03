using Production.Persistence.Repositories;
using Production.Domain.Entities;
using FileStorage.Contracts;
using Production.API.Features.Shared;
using Production.API.Features.UploadFiles.Shared;
using Mapster;
using Production.Application.StateMachines;

namespace Production.API.Features.UploadFiles.UploadFinalFile;

public class UploadFileEndpoint<TUploadCommand>
    (ArticleRepository articleRepository, AssetRepository _assetRepository, IFileService _fileService, AssetStateMachineFactory _factory)
    : BaseEndpoint<TUploadCommand, AssetActionResponse>(articleRepository)
    where TUploadCommand : UploadFileCommand
{
    //talk - readonly fields in primary constructors are not supported yet but they will be

    public async override Task HandleAsync(TUploadCommand command, CancellationToken ct)
    {
        var article = await _articleRepository.GetByIdWithSingleAssetAsync(command.ArticleId, command.AssetType, command.GetAssetNumber());
        var asset = article.Assets.SingleOrDefault();

        if (asset is null)
        {
						var assetTypeEntity = _assetRepository.GetAssetType(command.AssetType);

            //var stateMachine = _factory(Domain.Enums.AssetState.None);
						asset = Asset.CreateFromUpload(command, assetTypeEntity, command.GetAssetNumber());
        }

        var uploadResponse = await UploadFile(command, asset);

        asset.CreateAndAddFile(uploadResponse);

				await SendAsync(asset.Adapt<AssetActionResponse>());
		}

    private async Task<UploadResponse> UploadFile(UploadFileCommand command, Asset asset)
    {
        var filePath = $"{command.ArticleId}/{asset.Name}/{asset.Number}";
        //talk about tags
        return await _fileService.UploadFile(filePath, command.File,
                new Dictionary<string, string>{ 
                    {"entity", nameof(Asset)},
                    {"entityId", asset.Id.ToString()}
                });
    }
}
