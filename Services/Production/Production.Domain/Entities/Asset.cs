using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class Asset : AuditedEntity
{
    public static Asset CreateFromRequest(IArticleAction articleAction, AssetType assetType, byte assetNumber = 0)
    {
        //talk - the ideal implementation sould have been to actually encapsulate the AssetNumber in  its own class.
        // the problem is we need to validate against the assetType.MaxNumber, therefore we need to send the AssetType as a parameter.
        //todo - extract it into a value object
        ArgumentOutOfRangeException.ThrowIfGreaterThan(assetNumber, assetType.MaxNumber);

				var asset = new Asset()
        {
            Name = assetType.Name,
            TypeCode = assetType.Code,
            AssetNumber = assetNumber,
            CategoryId = assetType.DefaultCategoryId,
            Status = AssetStatus.Requested
        };

        var file = new File() { Name = $"{asset.Name}.{assetType.DefaultFileExtension}", StatusId = FileStatus.Requested };
        asset.Files.Add(file);
        if(asset.LatestFileRef is null) 
            asset.LatestFileRef = new AssetLatestFile() { Asset= asset, File = file };
        else
            asset.LatestFileRef.File = file;

        file.FileActions.Add(
            new FileAction() { CreatedById = articleAction.UserId, Comment = articleAction.Comment, CreatedOn = DateTime.UtcNow, TypeId = articleAction.ActionType }
            );
        return asset;
    }
		public static Asset CreateFromUpload(AssetType assetType, string originalFileName, byte assetNumber = 0)
    {
				var asset = new Asset()
				{
						Name = assetType.Name,
						TypeCode = assetType.Code,
						AssetNumber = assetNumber,
						CategoryId = assetType.DefaultCategoryId,
						Status = AssetStatus.Uploaded
				};

        return asset;
		}


		public string Name { get; init; } = null!;
		public int AssetNumber { get; init; }
    
    //talk - keep them as enum because they change quite rarely
    public AssetStatus Status { get; set; }
    public AssetCategory CategoryId { get; private set; }

    public int ArticleId { get; set; }
    public virtual Article Article { get; set; } = null!;

    public Enums.AssetType TypeCode { get; init; }
    public virtual AssetType Type { get; private set; } = null!;

    public virtual ICollection<File> Files { get; } = new List<File>();


    public int LatestFileId { get; private set; }
    public virtual AssetLatestFile LatestFileRef { get; private set; } = null!;

    public File LatestFile => this.LatestFileRef.File;
    public int LatestVersion => this.LatestFileRef?.File.Version ?? 0;
}
