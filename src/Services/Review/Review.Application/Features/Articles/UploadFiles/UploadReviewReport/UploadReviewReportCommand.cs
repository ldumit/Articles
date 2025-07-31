using Review.Application.Features.Articles.UploadFiles._Shared;
using Review.Domain.Articles.Enums;

namespace Review.Application.Features.Articles.UploadFiles.UploadReviewReport;

public record UploadReviewReportCommand : UploadFileCommand
{
		public override ArticleActionType ActionType => ArticleActionType.UploadReviewReport;
}

public abstract class UploadReviewReportCommandValidator : UploadFileValidator<UploadReviewReportCommand>
{
    public UploadReviewReportCommandValidator(AssetTypeRepository assetTypeRepository)
            : base(assetTypeRepository) { }

    public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.ReviewReport;
}