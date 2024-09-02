using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class Asset : AuditedEntity
{
    public string Name { get; set; } = null!;
		public int AssetNumber { get; set; }
    
    //talk - keep them as enum because they change quite rarely
    public AssetStatus Status { get; set; }
    public AssetCategory CategoryId { get; set; }

    public int ArticleId { get; set; }
    public virtual Article Article { get; set; } = null!;

    public Enums.AssetType TypeCode { get; set; }
    public virtual AssetType Type { get; set; } = null!;

    public virtual ICollection<File> Files { get; } = new List<File>();


    public int LatestFileId { get; set; }
    public virtual AssetLatestFile LatestFileRef { get; set; } = null!;

    public File LatestFile => this.LatestFileRef.File;
    public int LatestVersion => this.LatestFileRef?.File.Version ?? 0;
}
