using Articles.Entitities;
using Production.Domain.Enums;


namespace Production.Domain.Entities;

public partial class AssetType : EnumEntity2<Enums.AssetType>
{
    public int CategoryId { get; set; }
    public AssetCategory DefaultCategoryId { get; set; }
    public virtual AssetCategory? DefaultCategory { get; set; }
}
