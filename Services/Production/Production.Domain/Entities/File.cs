using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class File : TenantEntity
{
    public string OriginalName { get; set; } = null!;

    public string FileServerId { get; set; } = null!;

    public string? FormattedFileServerId => FileServerId?.Replace("/", "_").ToLower();

    public int Size { get; set; }

    public int Version { get; set; }

    public bool? IsLatest { get; set; }

    public DateTime LastActionDate { get; set; }

    public ArticleFileStatusCode StatusId { get; set; }

    public int LastActionUserId { get; set; }

    public string Name { get; set; } = null!;

    public string Extension { get; set; } = null!;

    public int AssetId { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public string? ErrorMessage { get; set; }

    public Guid? UploadBatchId { get; set; }
    public virtual Asset? Asset { get; set; }

    public virtual ICollection<FileAction> FileActions { get; } = new List<FileAction>();

    //public virtual ICollection<PublishedFile> PublishedFiles { get; } = new List<PublishedFile>();

    public virtual FileStatus? Status { get; set; }

}
