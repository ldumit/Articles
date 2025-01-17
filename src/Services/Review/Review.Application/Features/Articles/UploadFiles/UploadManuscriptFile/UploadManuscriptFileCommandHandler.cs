using FileStorage.Contracts;
using Review.Application.Features.Articles.UploadFiles._Shared;

namespace Review.Application.Features.Articles.UploadFiles.UploadManuscriptFile;

public class UploadManuscriptFileCommandHandler(
        ArticleRepository articleRepository,
        AssetTypeRepository assetTypeRepository,
        IFileService fileService,
        ArticleStateMachineFactory stateMachineFactory)
        : UploadFileCommandHandler<UploadManuscriptFileCommand>(articleRepository, assetTypeRepository, fileService, stateMachineFactory)
{
    protected override ArticleStage NextStage => ArticleStage.ManuscriptUploaded;
}
