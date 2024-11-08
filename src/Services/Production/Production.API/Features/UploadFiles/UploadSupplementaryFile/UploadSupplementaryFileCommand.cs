using Production.API.Features.UploadFiles.Shared;
using Production.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Production.API.Features.UploadFiles.UploadAuthorFile;

public record UploadSupplementaryFileCommand : UploadFileCommand
{
		[Required]
		public byte AssetNumber { get; set; }

		internal override byte GetAssetNumber() => AssetNumber;
}

public abstract class UploadSupplementaryFileValidator : UploadFileValidator<UploadSupplementaryFileCommand>
{
		public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.SupplementaryAssets;
}