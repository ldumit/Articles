using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.Persistence.Repositories;
using FileStorage.Contracts;
using Production.Application.StateMachines;
using Articles.Security;
using Production.API.Features.UploadFiles.Shared;

namespace Production.API.Features.UploadFiles.UploadFinalFile;

[Authorize(Roles = Role.TSOF)]
[HttpPost("articles/{articleId:int}/assets/final/files:upload")]
[AllowFileUploads]
[Tags("Files")]
public class UploadFinalFileEndpoint(ArticleRepository articleRepository, AssetRepository assetRepository, IFileService fileService, AssetStateMachineFactory factory)
    : UploadFileEndpoint<UploadFinalFileCommand>(articleRepository, assetRepository, fileService, factory)
{
}
