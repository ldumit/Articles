using Production.Persistence.Repositories;
using Production.Domain.Entities;
using FileStorage.Contracts;
using Production.API.Features.Shared;
using Mapster;
using Production.Application.StateMachines;
using Production.Application.Dtos;
using Production.Domain.Enums;

namespace Production.API.Features.UploadFiles.Shared;

public class UploadFileEndpoint<TUploadCommand>
    (ArticleRepository articleRepository, AssetTypeRepository assetTypeRepository, IFileService fileService, AssetStateMachineFactory factory)
    : AssetBaseEndpoint<TUploadCommand, AssetActionResponse>(assetTypeRepository, factory)
    where TUploadCommand : UploadFileCommand
{
		//talk - readonly fields in primary constructors are not supported yet but they will be
		protected readonly ArticleRepository _articleRepository = articleRepository;
		protected readonly IFileService _fileService = fileService;

		public async override Task HandleAsync(TUploadCommand command, CancellationToken ct)
    {
        //_article = await _articleRepository.GetByIdWithSingleAssetAsync(command.ArticleId, command.AssetType, command.GetAssetNumber());
				_article = await _articleRepository.GetByIdWithAssetsAsync(command.ArticleId);
				var assetType = _assetTypeRepository.GetById(command.AssetType);

				var asset = _article.Assets.SingleOrDefault(a => a.Type == command.AssetType && a.Number == command.GetAssetNumber());
				if (asset is null)
						asset = _article.CreateAsset(assetType, command.GetAssetNumber());

				CheckAndThrowStateTransition(asset, command.ActionType);
				asset.SetState(AssetState.Uploaded, command);

				var uploadResponse = await UploadFile(command, asset);
				try
				{
						asset.CreateAndAddFile(uploadResponse, assetType);

            _article.SetStage(NextStage, command);

				    await _articleRepository.SaveChangesAsync();
				}
				catch (Exception)
				{
            await _fileService.TryDeleteFileAsync(uploadResponse.FilePath); // delete the file if something is wrong
            throw;
				}

				await SendAsync(new AssetActionResponse(asset.Adapt<AssetMinimalDto>()));
		}

		private async Task<UploadResponse> UploadFile(UploadFileCommand command, Asset asset)
    {
        var filePath = asset.GenerateStorageFilePath(command.File.FileName);
        //talk about tags
        return await _fileService.UploadFileAsync(filePath, command.File, overwrite:true,
                new Dictionary<string, string>{ 
                    {"entity", nameof(Asset)},
                    {"entityId", asset.Id.ToString()}
                });
    }    
}
