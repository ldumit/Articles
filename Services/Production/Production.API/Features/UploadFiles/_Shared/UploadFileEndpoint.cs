using Production.Persistence.Repositories;
using Production.Domain.Entities;
using FileStorage.Contracts;
using Production.API.Features.Shared;
using Mapster;
using Production.Application.StateMachines;
using Production.Application.Dtos;

namespace Production.API.Features.UploadFiles.Shared;

public class UploadFileEndpoint<TUploadCommand>
    (ArticleRepository articleRepository, AssetRepository _assetRepository, IFileService _fileService, AssetStateMachineFactory _factory)
    : BaseEndpoint<TUploadCommand, AssetActionResponse>(articleRepository)
    where TUploadCommand : UploadFileCommand
{
    //talk - readonly fields in primary constructors are not supported yet but they will be
    public async override Task HandleAsync(TUploadCommand command, CancellationToken ct)
    {
        _article = await _articleRepository.GetByIdWithSingleAssetAsync(command.ArticleId, command.AssetType, command.GetAssetNumber());
        
        var asset = _article.Assets.SingleOrDefault();
        if (asset is null)
						asset = CreateAsset(command, _article);

				var uploadResponse = await UploadFile(command, asset);

        try
        {
            asset.CreateAndAddFile(uploadResponse);

            _article.SetStage(NextStage, command);

				    await _articleRepository.SaveChangesAsync();
				}
				catch (Exception)
				{
            await _fileService.TryDeleteFileAsync(uploadResponse.FilePath); // delete the file if 
            throw;
				}

				await SendAsync(new AssetActionResponse(asset.Adapt<AssetMinimalDto>()));
		}

		private Asset CreateAsset(TUploadCommand command, Article article)
		{
				var assetTypeEntity = _assetRepository.GetAssetType(command.AssetType);
				var asset = Asset.CreateFromUpload(command, assetTypeEntity, command.GetAssetNumber());
        _articleRepository.Context.Assets.Add(asset);
				//article.Assets.Add(asset);
				return asset;
		}

		private async Task<UploadResponse> UploadFile(UploadFileCommand command, Asset asset)
    {
        var filePath = asset.GenerateStorageFilePath(command.File.FileName);
        //talk about tags
        return await _fileService.UploadFileAsync(filePath, command.File,
                new Dictionary<string, string>{ 
                    {"entity", nameof(Asset)},
                    {"entityId", asset.Id.ToString()}
                });
    }    
}
