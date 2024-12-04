using FileStorage.Contracts;
using Review.Application.Features.UploadFiles.Shared;

namespace Review.Application.Features.UploadFiles.UploadReviewReport;

public class UploadReviewReportCommandHandler(
    ArticleRepository articleRepository,
		AssetTypeRepository assetTypeRepository,
		IFileService fileService,
		ArticleStateMachineFactory stateMachineFactory)
    : UploadFileCommandHandler<UploadReviewReportCommand>(articleRepository, assetTypeRepository, fileService, stateMachineFactory)
{
}
