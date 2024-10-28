using Submission.Persistence.Repositories;
using Submission.Domain.Entities;
using FileStorage.Contracts;
using Submission.Application.Features.Shared;
using Mapster;
using Submission.Application.StateMachines;
using Submission.Application.Dtos;
using Submission.Domain.Enums;

namespace Submission.Application.Features.UploadFiles.Shared;

public class UploadFileEndpoint<TUploadCommand>
    (ArticleRepository _articleRepository, AssetRepository assetRepository, IFileService _fileService, AssetStateMachineFactory factory)
    : AssetBaseEndpoint<TUploadCommand, AssetActionResponse>(assetRepository, factory)
    where TUploadCommand : UploadFileCommand
{
		//talk - readonly fields in primary constructors are not supported yet but they will be
		public async override Task<AssetActionResponse> Handle(TUploadCommand command, CancellationToken ct)
    {
        _article = await _articleRepository.GetByIdWithSingleAssetAsync(command.ArticleId, command.AssetType, command.GetAssetNumber());
				var asset = _article.Assets.SingleOrDefault();
				if (asset is null)
						asset = CreateAsset(command.AssetType, command.GetAssetNumber());

				CheckAndThrowStateTransition(asset, command.ActionType);
				asset.SetState(AssetState.Uploaded, command);

				var uploadResponse = await UploadFile(command, asset);
				try
				{
						asset.CreateAndAddFile(uploadResponse);

            _article.SetStage(NextStage, command);

				    await _articleRepository.SaveChangesAsync();
				}
				catch (Exception)
				{
            await _fileService.TryDeleteFileAsync(uploadResponse.FilePath); // delete the file if something is wrong
            throw;
				}

				return new AssetActionResponse(asset.Adapt<AssetMinimalDto>());
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
