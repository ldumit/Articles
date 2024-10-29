using Submission.Application.Features.UploadFiles.Shared;
using Submission.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Submission.Application.Features.UploadFiles;

public record UploadSupplementaryFileCommand : UploadFileCommand
{
}

public abstract class UploadSupplementaryFileValidator : UploadFileValidator<UploadSupplementaryFileCommand>
{
		public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.SupplementaryAssets;
}