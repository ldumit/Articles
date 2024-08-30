using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Production.API.Features.UploadAuthorsProof;
using Production.Application;
using Production.Persistence.Repositories;
using Production.Domain.Entities;
using Production.Domain.Enums;
using Articles.Exceptions;
using System.Net;
using Azure;
using System;
using AssetType = Production.Domain.Entities.AssetType;
using FileStorage.Contracts;

namespace Production.API.Features;

[Authorize(Roles = "TSOF")]
[HttpPut("articles/{articleId:int}/typesetter")]
public class UploadFinalFileEndpoint(IFileService _fileService, IServiceProvider serviceProvider)
    : BaseEndpoint<UploadFinalFileCommand, UploadFileResponse>(serviceProvider)
{
		protected readonly AssetRepository _assetRepository;

		public async override Task HandleAsync(UploadFinalFileCommand command, CancellationToken ct)
		{
				using var transaction = _articleRepository.BeginTransaction();

				var article = _articleRepository.GetById(command.ArticleId);
				
				var asset = FindEntity(command);
				bool isNew = asset is null;
				if (isNew)
						asset = CreateEntity(command);

				var uploadSession = await UploadFile(command);

				//_fileService.UploadFile(asset, uploadSession, ct);
				//asset.IsNewVersion;
				//asset.IsFileRequested;
		}

		private async Task<UploadResponse> UploadFile(UploadFileCommand command)
		{
				using var fileContent = command.File.OpenReadStream();
				return await _fileService.UploadFile(command.FileServerId,
																															 command.File.FileName,
																															 _assetProvider.ContentType(command.File.ContentType),
																															 fileContent);
		}
		protected virtual Asset FindEntity(UploadFileCommand command)
		{
				return _assetRepository.GetByTypeAndNumber(command.ArticleId, command.AssetType, command.GetAssetNumber());
		}

		protected virtual Asset CreateEntity(UploadFileCommand command)
		{
				var assetType = _assetRepository.GetAssetType(command.AssetType);

				var asset = new Asset()
				{
						Name = assetType.Name,
						Type = assetType,
						CategoryId = assetType.DefaultCategoryId,
						Status = AssetStatus.Uploaded,
						ArticleId = command.ArticleId,
						AssetNumber = command.GetAssetNumber(),
				};


				//_mapper
				//		.MultiMap(command, ref asset)
				//		.MultiMap(_assetProvider, ref asset);
				//_assetRepository.Add(asset);

				//CreateFileNameAndFileServerId(command);
				return asset;
		}
}
