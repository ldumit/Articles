using Production.API.Features.UploadFiles.Shared;
using Production.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Production.API.Features.UploadFiles.UploadAuthorFile;

public record UploadAuthorFileCommand : UploadFileCommand
{
		[Required]
		public byte AssetNumber { get; set; }

		internal override byte GetAssetNumber() => AssetNumber;
}

public abstract class UploadAuthorFileValidator : UploadFileValidator<UploadAuthorFileCommand>
{
		public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.AuthorFiles;
}