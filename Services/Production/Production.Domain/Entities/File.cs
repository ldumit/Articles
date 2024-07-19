using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class File : AuditedEntity
{
    //talk - difference between required, null!, default!
    public required string OriginalName { get; set; }
    
    //talk - server id, create the file server server both mongo & azure blob
    public required string FileServerId { get; set; } = default!;

    public string FormattedFileServerId => FileServerId.Replace("/", "_").ToLower();

    public int Size { get; set; }

    public int Version { get; set; }

    public FileStatus StatusId { get; set; }

    public required string Name { get; set; }

    public string Extension { get; set; } = default!;

    public int AssetId { get; set; }

    public virtual Asset? Asset { get; set; }

    public int LatestActionId { get; set; }
    public FileLatestAction LatestAction { get; set; } = null!;
    public virtual ICollection<FileAction> FileActions { get; } = new List<FileAction>();

    //public virtual ICollection<PublishedFile> PublishedFiles { get; } = new List<PublishedFile>();
}
