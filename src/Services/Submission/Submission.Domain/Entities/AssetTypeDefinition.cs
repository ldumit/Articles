using Articles.Abstractions.Enums;
using Blocks.Entitities;
using Blocks.Core.Cache;
using Submission.Domain.ValueObjects;


namespace Submission.Domain.Entities;

//talk - mix enums & tables togheter
public partial class AssetTypeDefinition : EnumEntity<AssetType>, ICacheable
{
		public required AssetCategory DefaultCategoryId { get; init; }
    public required FileExtensions AllowedFileExtensions { get; init; }
		public required string DefaultFileExtension { get; init; } = default!;
    public required byte MaxNumber { get; init; }
		public required byte MaxFileSizeInMB{ get; init; }

		public int MaxFileSizeInBytes => (MaxFileSizeInMB * 1024 * 1024);
		public bool AllowsMultipleAssets => MaxNumber > 0;
}
