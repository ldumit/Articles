using Review.Application.Features.UploadFiles.Shared;

namespace Review.Application.Features.UploadFiles;

public record UploadReviewReportCommand : UploadFileCommand;

public abstract class UploadReviewReportCommandValidator : UploadFileValidator<UploadReviewReportCommand>
{
		public UploadReviewReportCommandValidator(AssetTypeRepository assetTypeRepository)
				: base(assetTypeRepository) { }

		public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.ReviewReport;
}