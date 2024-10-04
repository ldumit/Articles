using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.Persistence.Repositories;
using FileStorage.Contracts;
using Production.Application.StateMachines;
using Articles.Security;
using Production.API.Features.UploadFiles.Shared;

namespace Production.API.Features.UploadFiles.UploadAuthorFile;

[Authorize(Roles = Role.AUT)]
[HttpPost("articles/{articleId:int}/author-files:upload")]
[AllowFileUploads]
public class UploadAuthorFileEndpoint(ArticleRepository articleRepository, AssetRepository assetRepository, IFileService fileService, AssetStateMachineFactory factory)
    : UploadFileEndpoint<UploadAuthorFileCommand>(articleRepository, assetRepository, fileService, factory)
{
}
