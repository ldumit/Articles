using Articles.Entitities;
using Production.Domain.Enums;
using Production.Domain.ValueObjects;

namespace Production.Domain.Entities;

public partial class Asset : AggregateEntity
{
		public AssetName Name { get; private set; } = null!;
    public AssetNumber Number { get; private set; } = null!;    
    //talk - keep them as enum because they change quite rarely
    public AssetState State { get; private set; }
    public AssetCategory CategoryId { get; private set; }

    public Enums.AssetType Type { get; private set; }
    public virtual AssetType TypeRef { get; private set; } = null!;
		
    public int ArticleId { get; private set; }
		public virtual Article Article { get; private set; } = null!;


		private readonly List<File> _files = new();
		public virtual IReadOnlyList<File> Files => _files.AsReadOnly();

		
    private readonly List<AssetAction> _actions = new ();
		public virtual IReadOnlyList<AssetAction> Actions => _actions.AsReadOnly();

    
    public virtual AssetCurrentFileLink? CurrentFileLink { get; private set; } = null!;
    public File? CurrentFile => this.CurrentFileLink?.File;
}
