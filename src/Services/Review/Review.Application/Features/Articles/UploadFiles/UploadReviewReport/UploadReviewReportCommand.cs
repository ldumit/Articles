using Review.Application.Features.Articles.UploadFiles._Shared;

namespace Review.Application.Features.Articles.UploadFiles.UploadReviewReport;

public record UploadReviewReportCommand : UploadFileCommand;

public abstract class UploadReviewReportCommandValidator : UploadFileValidator<UploadReviewReportCommand>
{
    public UploadReviewReportCommandValidator(AssetTypeRepository assetTypeRepository)
            : base(assetTypeRepository) { }

    public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.ReviewReport;
}