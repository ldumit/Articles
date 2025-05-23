﻿using Blocks.Core;
using FileStorage.Contracts;

namespace Submission.Application.Features.DownloadFile;

public class DownloadFileCommandHandler(AssetRepository _assetRepository, IFileService _fileService) : IRequestHandler<DownloadFileQuery, DownloadFileResponse>
{
		public async Task<DownloadFileResponse> Handle(DownloadFileQuery command, CancellationToken cancellationToken)
		{
				var asset = Guard.NotFound(await _assetRepository.GetByIdAsync(command.ArticleId, command.AssetId));

				var (fileStream, contentType) = await _fileService.DownloadFileAsync(asset!.File.FileServerId);

				return new DownloadFileResponse(asset.File.Name, contentType, fileStream);
		}
}