using Articles.Entitities;
using Production.Domain.Enums;


namespace Production.Domain.Entities;

public partial class AssetType : EnumEntity<ArticleAssetType>
{
    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public string Code { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public AssetCategory DefaultCategoryId { get; set; }
    public virtual AssetCategory? DefaultCategory { get; set; }

}
