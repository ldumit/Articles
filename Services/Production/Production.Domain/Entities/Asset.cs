using Articles.Entitities;
using Production.Domain.Enums;
using System;

namespace Production.Domain.Entities;

public partial class Asset : AuditedEntity
{
		public string Name { get; init; } = null!;
		public int AssetNumber { get; init; }
    
    //talk - keep them as enum because they change quite rarely
    public AssetStatus Status { get; set; }
    public AssetCategory CategoryId { get; private set; }

    public int ArticleId { get; set; }
    public virtual Article Article { get; set; } = null!;

    public Enums.AssetType TypeCode { get; init; }
    public virtual AssetType Type { get; private set; } = null!;

    public virtual ICollection<File> Files { get; init; } = new List<File>();

		
		private readonly List<AssetAction> _actions = new ();
		public virtual IReadOnlyCollection<AssetAction> Actions => _actions.AsReadOnly();

    public virtual AssetCurrentFileLink? CurrentFileLink { get; set; } = null!;
    public File? CurrentFile => this.CurrentFileLink?.File;
}
