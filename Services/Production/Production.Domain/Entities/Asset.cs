using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class Asset : AuditedEntity
{
    public required string Name { get; set; }
    public int AssetNumber { get; set; }
    
    //talk - keep them as enum because they change quite rarely
    public AssetStatus StatusId { get; set; }
    public AssetCategory CategoryId { get; set; }

    public int ArticleId { get; set; }
    public virtual Article Article { get; set; } = null!;

    public Enums.AssetType TypeId { get; set; }
    public virtual AssetType Type { get; set; } = null!;

    public virtual ICollection<File> Files { get; } = new List<File>();


    public int LatestFileId { get; set; }
    public virtual AssetLatestFile LatestFile { get; set; } = null!;
    public int LatestVersion => this.LatestFile?.File.Version ?? 0;
}
