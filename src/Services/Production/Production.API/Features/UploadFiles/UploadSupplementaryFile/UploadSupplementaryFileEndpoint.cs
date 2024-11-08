using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.Persistence.Repositories;
using FileStorage.Contracts;
using Production.Application.StateMachines;
using Articles.Security;
using Production.API.Features.UploadFiles.Shared;

namespace Production.API.Features.UploadFiles.UploadAuthorFile;

[Authorize(Roles = Role.CORAUT)]
[HttpPost("articles/{articleId:int}/assets/supplementary/files:upload")]
[AllowFileUploads]
[Tags("Files")]
public class UploadSupplementaryFileEndpoint(ArticleRepository articleRepository, AssetRepository assetRepository, IFileService fileService, AssetStateMachineFactory factory)
    : UploadFileEndpoint<UploadSupplementaryFileCommand>(articleRepository, assetRepository, fileService, factory)
{
}
