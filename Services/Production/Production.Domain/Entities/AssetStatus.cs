using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class AssetStatus : EnumEntity<ArticleAssetStatusCode>
{
    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public virtual ICollection<Asset> Assets { get; } = new List<Asset>();

    public virtual AssetStatusCode CodeNavigation { get; set; } = null!;
}
