using FileStorage.Contracts;

namespace Submission.Application.Features.UploadFiles.Shared;

public class UploadFileCommandHandler<TUploadCommand>
    (ArticleRepository _articleRepository, CachedAssetRepo _assetTypeRepository, IFileService _fileService, ArticleStateMachineFactory _stateMachineFactory)
    : IRequestHandler<TUploadCommand, IdResponse>
		where TUploadCommand : UploadFileCommand
{
		protected Article _article = null!;

		public virtual async Task<IdResponse> Handle(TUploadCommand command, CancellationToken ct)
    {
        _article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

				var assetType = _assetTypeRepository.GetById(command.AssetType);
				var asset = _article.GetOrCreateAsset(assetType, command);

				_article.SetStage(NextStage, command, _stateMachineFactory);

				var uploadResponse = await UploadFile(command, asset, assetType);
				
				try
				{
						asset.CreateFile(uploadResponse, assetType, command);

						await _articleRepository.SaveChangesAsync();
				}
				catch (Exception)
				{
            await _fileService.TryDeleteFileAsync(uploadResponse.FilePath); // delete the file if something is wrong
            throw;
				}

				return new IdResponse(asset.Id);
		}

		protected virtual ArticleStage NextStage => _article!.Stage;

		protected async Task<UploadResponse> UploadFile(UploadFileCommand command, Asset asset, AssetTypeDefinition assetType)
    {
        var filePath = asset.GenerateStorageFilePath(command.File.FileName);
        //talk about tags
        return await _fileService.UploadFileAsync(
						filePath, 
						command.File, 
						//overwrite: !assetType.AllowsMultipleAssets, // if the asset type does not support multiple assets, we are overriding the file.
						overwrite: true,
						tags: new Dictionary<string, string>{ 
                    {"entity", nameof(Asset)},
                    {"entityId", asset.Id.ToString()}
                });
    }
}
