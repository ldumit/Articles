using FileStorage.Contracts;
using Review.Application.Features.UploadFiles.Shared;

namespace Review.Application.Features.UploadFiles.UploadDraftFile;

public class UploadManuscriptFileCommandHandler(
		ArticleRepository articleRepository,
		AssetTypeRepository assetTypeRepository, 
		IFileService fileService,
		ArticleStateMachineFactory stateMachineFactory)
		: UploadFileCommandHandler<UploadManuscriptFileCommand>(articleRepository, assetTypeRepository, fileService, stateMachineFactory)
{
		protected override ArticleStage NextStage => ArticleStage.ManuscriptUploaded;
}
