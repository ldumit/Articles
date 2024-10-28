using Submission.Persistence.Repositories;
using FileStorage.Contracts;
using Submission.Application.StateMachines;
using Submission.Application.Features.UploadFiles.Shared;
using Articles.Abstractions;

namespace Submission.Application.Features.UploadFiles.UploadDraftFile;

public class UploadDraftFileEndpoint(ArticleRepository articleRepository, AssetRepository assetRepository, IFileService fileService, AssetStateMachineFactory factory)
		: UploadFileEndpoint<UploadDraftFileCommand>(articleRepository, assetRepository, fileService, factory)
{

		protected override ArticleStage NextStage => ArticleStage.DraftProduction;
}
