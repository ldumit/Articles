using Articles.Entitities;
using Production.Domain.Enums;


namespace Production.Domain.Entities;

//talk - mix enums & tables togheter
public partial class AssetType : EnumEntity<Enums.AssetType>
{
    public AssetCategory DefaultCategoryId { get; set; }
    public virtual AssetCategory DefaultCategory { get; set; } = default!;
}
