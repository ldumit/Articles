using Production.Database.Repositories;
using Production.Domain.Enums;
using Articles.System;

namespace Production.Application;

public abstract class AssetProviderBase
{
    protected readonly AssetRepository _assetRepository;

    protected AssetProviderBase(AssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public async virtual Task<bool> IsActionValid(ArticleStagesCode stage, FileActionTypeCode action) => true;
        //=> (await GetAvailableActions(stage)).Any(a => a == action);


    public abstract ArticleAssetType AssetType { get; }
    public virtual AssetCategory DefaultCategory => AssetCategory.CORE;
    public virtual string AssetName => Description;
    public string Description => AssetType.ToDescription();

    public virtual string ContentType(string contentType) => contentType;
    public virtual long MaxFileSize => 10 * 1024; //10MB
    public virtual string CreateFileName(string fileExtension) => $"{AssetName}.{fileExtension}";

    public virtual string CreateFileServerId(int articleId, string fileExtension = null) => $"{articleId}/{Description.ToLower().Replace("'", "").Replace(" ", "-")}";
    public virtual ArticleFileStatusCode DefaultFileStatusId => ArticleFileStatusCode.UPLOADED;
    public virtual int AssetNumber { get; protected set; }
    public string CreateSubmissionName(string fileName, string fileExtension)
        => Path.GetExtension(fileName) == string.Empty ? $"{fileName}.{fileExtension}" : fileName;
}


