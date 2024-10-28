using Submission.Persistence.Repositories;
using FileStorage.Contracts;
using Submission.Application.StateMachines;
using Submission.Application.Features.UploadFiles.Shared;

namespace Submission.Application.Features.UploadFiles.UploadAuthorFile;

public class UploadSupplementaryFileEndpoint(ArticleRepository articleRepository, AssetRepository assetRepository, IFileService fileService, AssetStateMachineFactory factory)
    : UploadFileEndpoint<UploadSupplementaryFileCommand>(articleRepository, assetRepository, fileService, factory)
{
}
