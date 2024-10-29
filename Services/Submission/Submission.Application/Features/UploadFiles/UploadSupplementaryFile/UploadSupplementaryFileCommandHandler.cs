using Submission.Persistence.Repositories;
using FileStorage.Contracts;
using Submission.Application.Features.UploadFiles.Shared;
using Submission.Domain.Entities;
using Submission.Domain.Enums;
using Submission.Domain.StateMachines;

namespace Submission.Application.Features.UploadFiles.UploadAuthorFile;

public class UploadSupplementaryFileCommandHandler(
    ArticleRepository articleRepository,
		CachedRepository<AssetTypeDefinition, AssetType> assetTypeRepository,
		IFileService fileService,
		ArticleStateMachineFactory stateMachineFactory)
    : UploadFileCommandHandler<UploadSupplementaryFileCommand>(articleRepository, assetTypeRepository, fileService, stateMachineFactory)
{
}
