using Production.API.Features.UploadFiles.Shared;
using Production.Domain.Enums;

namespace Production.API.Features.UploadFiles.UploadFinalFile;

public record UploadAuthorFileCommand : UploadFileCommand;

public abstract class UploadAuthorFileValidator : UploadFileValidator<UploadFinalFileCommand>
{
		public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.AuthorFiles;
}