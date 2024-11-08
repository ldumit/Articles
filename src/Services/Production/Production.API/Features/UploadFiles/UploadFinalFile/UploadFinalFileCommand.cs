using Production.API.Features.UploadFiles.Shared;
using Production.Domain.Enums;

namespace Production.API.Features.UploadFiles.UploadFinalFile;

public record UploadFinalFileCommand : UploadFileCommand;

public abstract class UploadFinalFileValidator : UploadFileValidator<UploadFinalFileCommand>
{
		public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.FinalAssets;
}