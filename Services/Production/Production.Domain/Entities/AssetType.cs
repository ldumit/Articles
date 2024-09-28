using Articles.Entitities;
using Production.Domain.Enums;
using Production.Domain.ValueObjects;


namespace Production.Domain.Entities;

//talk - mix enums & tables togheter
public partial class AssetType : EnumEntity<Enums.AssetType>
{
		public AssetCategory DefaultCategoryId { get; init; }
    public AllowedFileExtensions AllowedFileExtensions { get; init; } = null!;
		public string DefaultFileExtension { get; init; } = default!;
    public byte MaxNumber { get; init; }
}
