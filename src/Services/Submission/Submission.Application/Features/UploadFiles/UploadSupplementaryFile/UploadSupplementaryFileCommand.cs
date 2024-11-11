using Articles.Abstractions.Enums;
using Submission.Application.Features.UploadFiles.Shared;
using Submission.Domain.Entities;
using Submission.Domain.Enums;
using Submission.Persistence.Repositories;

namespace Submission.Application.Features.UploadFiles;

public record UploadSupplementaryFileCommand : UploadFileCommand;

public abstract class UploadSupplementaryFileValidator : UploadFileValidator<UploadSupplementaryFileCommand>
{
		public UploadSupplementaryFileValidator(CachedRepository<AssetTypeDefinition, AssetType> assetTypeRepository)
				: base(assetTypeRepository) { }

		public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.SupplementaryAssets;
}