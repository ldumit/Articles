using FileStorage.Contracts;
using Submission.Persistence.Repositories;
using Submission.Application.Features.UploadFiles.Shared;
using Submission.Domain.StateMachines;

namespace Submission.Application.Features.UploadFiles.UploadAuthorFile;

public class UploadSupplementaryFileCommandHandler(
    ArticleRepository articleRepository,
		AssetTypeRepository assetTypeRepository,
		IFileService fileService,
		ArticleStateMachineFactory stateMachineFactory)
    : UploadFileCommandHandler<UploadSupplementaryFileCommand>(articleRepository, assetTypeRepository, fileService, stateMachineFactory)
{
}
