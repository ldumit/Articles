using Submission.Persistence.Repositories;
using FileStorage.Contracts;
using Submission.Application.Features.UploadFiles.Shared;
using Submission.Domain.Entities;
using Submission.Domain.Enums;
using Submission.Domain.StateMachines;
using Articles.Abstractions;

namespace Submission.Application.Features.UploadFiles.UploadDraftFile;

public class UploadManuscriptFileCommandHandler(
		ArticleRepository articleRepository, 
		CachedRepository<AssetTypeDefinition, AssetType> assetTypeRepository, 
		IFileService fileService,
		ArticleStateMachineFactory stateMachineFactory)
		: UploadFileCommandHandler<UploadManuscriptFileCommand>(articleRepository, assetTypeRepository, fileService, stateMachineFactory)
{
		protected override ArticleStage NextStage => ArticleStage.ManuscriptUploaded;
}
