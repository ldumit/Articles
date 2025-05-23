﻿using FileStorage.Contracts;
using Production.Persistence.Repositories;
using Production.Application.StateMachines;
using Production.API.Features.UploadFiles.Shared;

namespace Production.API.Features.UploadFiles.UploadAuthorFile;

[Authorize(Roles = Role.CORAUT)]
[HttpPost("articles/{articleId:int}/assets/supplementary/files:upload")]
[AllowFileUploads]
[Tags("Files")]
public class UploadSupplementaryFileEndpoint(
    ArticleRepository articleRepository, AssetTypeRepository assetTypeRepository, IFileService fileService, AssetStateMachineFactory factory)
    : UploadFileEndpoint<UploadSupplementaryFileCommand>(articleRepository, assetTypeRepository, fileService, factory)
{
}
