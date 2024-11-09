using Submission.Persistence.Repositories;
using Submission.Domain.Enums;
using Submission.Application.Features.UploadFiles.Shared;
using Submission.Domain.Entities;

namespace Submission.Application.Features.UploadFiles;

public record UploadManuscriptFileCommand : UploadFileCommand;

public class UploadManuscriptCommandValidator : UploadFileValidator<UploadManuscriptFileCommand>
{
		public UploadManuscriptCommandValidator(CachedRepository<AssetTypeDefinition, AssetType> assetTypeRepository)
				: base(assetTypeRepository) { }

		public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.ManuscriptAsset;
}
