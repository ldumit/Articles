using Articles.Entitities;
using Articles.System.Cache;
using Submission.Domain.Enums;
using Submission.Domain.ValueObjects;


namespace Submission.Domain.Entities;

//talk - mix enums & tables togheter
public partial class AssetType : EnumEntity<Enums.AssetType>, ICacheable
{
		public AssetCategory DefaultCategoryId { get; init; }
    public AllowedFileExtensions AllowedFileExtensions { get; init; } = null!;
		public string DefaultFileExtension { get; init; } = default!;
    public byte MaxNumber { get; init; }
		public byte MaxFileSizeInMB{ get; init; }
}
