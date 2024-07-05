using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class Asset : TenantEntity
{
    public AssetStatus StatusId { get; set; }

    public int ArticleId { get; set; }

    public Enums.AssetType TypeId { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public int? ProductionArticleAssetNumber { get; set; }

    public AssetCategory CategoryId { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string? Name { get; set; }

    public int? AssetNumber { get; set; }

    public virtual Article? Article { get; set; }

    public virtual ICollection<File> Files { get; } = new List<File>();

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual AssetType Type { get; set; } = null!;


    public int? LatestFileId { get; set; }

    public virtual Entities.File LatestFile { get; set; }
    public int LatestVersion => this.LatestFile?.Version ?? 0;

}
