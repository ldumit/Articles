using Review.Application.Features.Articles.UploadFiles._Shared;
using Review.Domain.Articles.Enums;

namespace Review.Application.Features.Articles.UploadFiles.UploadManuscriptFile;

public record UploadManuscriptFileCommand : UploadFileCommand
{
		public override ArticleActionType ActionType => ArticleActionType.UploadReviewReport;
}

public class UploadManuscriptCommandValidator : UploadFileValidator<UploadManuscriptFileCommand>
{
    public UploadManuscriptCommandValidator(AssetTypeRepository assetTypeRepository)
            : base(assetTypeRepository) { }

    public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.ManuscriptAsset;
}
