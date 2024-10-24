using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Submission.Persistence.Repositories;
using FileStorage.Contracts;
using Submission.Application.StateMachines;
using Articles.Security;
using Submission.API.Features.UploadFiles.Shared;
using Articles.Abstractions;

namespace Submission.API.Features.UploadFiles.UploadDraftFile;

[Authorize(Roles = Role.TSOF)]
[HttpPost("articles/{articleId:int}/assets/draft/files:upload")]
[AllowFileUploads]
[Tags("Files")]
public class UploadDraftFileEndpoint(ArticleRepository articleRepository, AssetRepository assetRepository, IFileService fileService, AssetStateMachineFactory factory)
		: UploadFileEndpoint<UploadDraftFileCommand>(articleRepository, assetRepository, fileService, factory)
{

		protected override ArticleStage NextStage => ArticleStage.DraftProduction;
}
