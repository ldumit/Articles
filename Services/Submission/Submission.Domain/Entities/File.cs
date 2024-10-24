using Articles.Entitities;
using Submission.Domain.ValueObjects;

namespace Submission.Domain.Entities;

public partial class File : AggregateEntity
{
    //talk - difference between required, null!, default!
    public string OriginalName { get; init; } = default!;
    
    //talk - server id, create the file server server both mongo & azure blob
    public string FileServerId { get; init; } = default!;

    public long Size { get; set; }

    public FileVersion Version { get; init; } = null!;

    public required FileName Name { get; init; }

		public FileExtension Extension { get; init; } = default!;

		public int AssetId { get; private set; }

    public virtual Asset? Asset { get; private set; }

		//public FileStatus StatusId { get; init; }
		//public int LatestActionId { get; set; }
		//public FileLatestAction LatestAction { get; set; } = null!;
		//public virtual ICollection<FileAction> FileActions { get; } = new List<FileAction>();
}
