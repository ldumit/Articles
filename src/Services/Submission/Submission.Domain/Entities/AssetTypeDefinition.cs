using Blocks.Core.Cache;

namespace Submission.Domain.Entities;

//talk - mix enums & tables togheter
public partial class AssetTypeDefinition : EnumEntity<AssetType>, ICacheable
{
		public required AssetCategory DefaultCategoryId { get; init; }
    public required FileExtensions AllowedFileExtensions { get; init; }
		public required string DefaultFileExtension { get; init; } = default!;
    public required byte MaxAssetCount { get; init; }
		public required byte MaxFileSizeInMB{ get; init; }

		public int MaxFileSizeInBytes => (MaxFileSizeInMB * 1024 * 1024);
		public bool AllowsMultipleAssets => MaxAssetCount > 1;
}
