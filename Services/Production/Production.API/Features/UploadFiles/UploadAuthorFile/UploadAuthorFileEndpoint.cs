using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.Persistence.Repositories;
using FileStorage.Contracts;
using Production.Application.StateMachines;
using Articles.Security;

namespace Production.API.Features.UploadFiles.UploadFinalFile;

[Authorize(Roles = Role.AUT)]
[HttpPost("articles/{articleId:int}/author-files:upload")]
public class UploadAuthorFileEndpoint(ArticleRepository articleRepository, AssetRepository assetRepository, IFileService fileService, AssetStateMachineFactory factory)
    : UploadFileEndpoint<UploadAuthorFileCommand>(articleRepository, assetRepository, fileService, factory)
{
}
