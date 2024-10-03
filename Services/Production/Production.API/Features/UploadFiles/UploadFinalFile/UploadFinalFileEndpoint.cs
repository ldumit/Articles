using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.Persistence.Repositories;
using FileStorage.Contracts;
using Production.Application.StateMachines;
using Articles.Security;

namespace Production.API.Features.UploadFiles.UploadFinalFile;

[Authorize(Roles = Role.TSOF)]
[HttpPost("articles/{articleId:int}/final-files:upload")]
public class UploadFinalFileEndpoint(ArticleRepository articleRepository, AssetRepository assetRepository, IFileService fileService, AssetStateMachineFactory factory)
    : UploadFileEndpoint<UploadFinalFileCommand>(articleRepository, assetRepository, fileService, factory)
{
}
