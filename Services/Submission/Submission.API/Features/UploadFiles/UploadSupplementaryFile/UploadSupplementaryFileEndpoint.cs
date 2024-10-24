using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Submission.Persistence.Repositories;
using FileStorage.Contracts;
using Submission.Application.StateMachines;
using Articles.Security;
using Submission.API.Features.UploadFiles.Shared;

namespace Submission.API.Features.UploadFiles.UploadAuthorFile;

[Authorize(Roles = Role.CORAUT)]
[HttpPost("articles/{articleId:int}/assets/supplementary/files:upload")]
[AllowFileUploads]
[Tags("Files")]
public class UploadSupplementaryFileEndpoint(ArticleRepository articleRepository, AssetRepository assetRepository, IFileService fileService, AssetStateMachineFactory factory)
    : UploadFileEndpoint<UploadSupplementaryFileCommand>(articleRepository, assetRepository, fileService, factory)
{
}
