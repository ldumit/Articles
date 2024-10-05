using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.Persistence.Repositories;
using FileStorage.Contracts;
using Production.Application.StateMachines;
using Articles.Security;
using Production.API.Features.UploadFiles.Shared;

namespace Production.API.Features.UploadFiles.UploadDraftFile;

[Authorize(Roles = Role.TSOF)]
[HttpPost("articles/{articleId:int}/assets/draft/files:upload")]
[AllowFileUploads]
[Tags("Files")]
public class UploadDraftFileEndpoint(ArticleRepository articleRepository, AssetRepository assetRepository, IFileService fileService, AssetStateMachineFactory factory)
		: UploadFileEndpoint<UploadDraftFileCommand>(articleRepository, assetRepository, fileService, factory)
{
}
