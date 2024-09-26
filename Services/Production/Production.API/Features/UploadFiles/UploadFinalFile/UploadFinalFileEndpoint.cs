using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.Persistence.Repositories;
using Production.Domain.Entities;
using Production.Domain.Enums;
using FileStorage.Contracts;
using Production.API.Features.Shared;
using Production.API.Features.UploadFiles.Shared;
using Production.Domain;
using Articles.Abstractions;

namespace Production.API.Features.UploadFiles.UploadFinalFile;

[Authorize(Roles = "TSOF")]
[HttpPut("articles/{articleId:int}/final-files:upload")]
public class UploadFinalFileEndpoint(IFileService _fileService, IServiceProvider serviceProvider)
    : BaseEndpoint<UploadFinalFileCommand, FileResponse>(serviceProvider)
{
    protected readonly AssetRepository _assetRepository;

    public async override Task HandleAsync(UploadFinalFileCommand command, CancellationToken ct)
    {
        var article = await _articleRepository.GetByIdWithSingleAssetAsync(command.ArticleId, command.AssetType, command.GetAssetNumber());
        var asset = article.Assets.SingleOrDefault();

				bool isNew = asset is null;
        if (isNew)
            asset = CreateAsset(command, command.AssetType, command.GetAssetNumber());

        var uploadResponse = await UploadFile(command, asset);

        //_fileService.UploadFile(asset, uploadSession, ct);
        //asset.IsNewVersion;
        //asset.IsFileRequested;
    }

    private async Task<UploadResponse> UploadFile(UploadFileCommand command, Asset asset)
    {
        var filePath = $"{command.ArticleId}/{asset.Name}/{asset.AssetNumber}";
        //talk about tags
        return await _fileService.UploadFile(filePath, command.File,
                new Dictionary<string, string>{ 
                    {"entity", nameof(Asset)},
                    {"entityId", asset.Id.ToString()}
                });
    }

    private async Task<Domain.Entities.File> CreateFile(UploadFileCommand command, Asset asset)
    {
        //await CheckAndCompleteRequestedFile(asset);

        //var latestFile = new File();
        //_mapper
        //		.MultiMap(command, ref latestFile)
        //		.MultiMap(uploadSession, ref latestFile)
        //		;
        //if (validatedZipContent != null && !validatedZipContent.ZipContent.IsNullOrEmpty())
        //{
        //		foreach (var content in validatedZipContent.ZipContent)
        //		{
        //				latestFile.ZipContentFiles.Add(_mapper.Map<ZipContentFile>(content));
        //		}
        //		latestFile.ErrorMessage = GetXmlErrorMessage(validatedZipContent.ZipContent);
        //}
        //asset.Files.Add(latestFile);
        ////asset.LatestFile = latestFile;
        //return latestFile;

        return new Domain.Entities.File() { FileServerId = "", Name = "", OriginalName = "" };
    }

    protected virtual async Task<Asset> FindAsset(UploadFileCommand command)
    {
        return await _assetRepository.GetByTypeAndNumberAsync(command.ArticleId, command.AssetType, command.GetAssetNumber(), throwNotFound: false);
    }

		protected virtual Asset CreateAsset(IArticleAction<AssetActionType> action, Domain.Enums.AssetType assetType, byte assetNumber)
		{
				var assetTypeEntity = _assetRepository.GetAssetType(assetType);

				return Asset.CreateFromRequest(action, assetTypeEntity, assetNumber);
		}
}
