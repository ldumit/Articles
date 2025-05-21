namespace Submission.Domain.Entities;

public partial class Asset : AggregateEntity
{
		public AssetName Name { get; private set; } = null!;
    //talk - keep the following properties as enums because they change quite rarely
    public AssetState State { get; private set; }
    public AssetCategory CategoryId { get; private set; }

    public AssetType Type { get; private set; }
    public virtual AssetTypeDefinition TypeRef { get; private set; } = null!;
		
    public int ArticleId { get; private set; }
		public virtual Article Article { get; private set; } = null!;

		public File File { get; set; } = null!; 
}
