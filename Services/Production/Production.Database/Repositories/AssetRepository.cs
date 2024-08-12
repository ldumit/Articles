using Articles.EntityFrameworkCore;
using Articles.Exceptions;
using Articles.System;
using Microsoft.EntityFrameworkCore;
using Production.Domain.Entities;
using Production.Domain.Enums;
using System.Net;

namespace Production.Persistence.Repositories;

public class AssetRepository : RepositoryBase<Asset>
{
    private readonly ICacheService _cacheService;
    private const string ASSET_CATEGORY_MAPPING_FAILED = "Asset category mapping failed.";

    public AssetRepository(DbContext dbContext, ICacheService cacheService) : base(dbContext, dbContext)
    {
        _cacheService = cacheService;
    }

    protected override IQueryable<Asset> Query()
    {
        return base.Entity.Include(e => e.LatestFile);
    }

    public override Asset GetById(int id, bool throwNotFound = false)
    {
        var entity = Query().SingleOrDefault(e => e.Id == id);
        //todo - Remove HttpException and HttpStatusCode ?! maybe??
        if (throwNotFound && entity is null)
            throw new HttpException(HttpStatusCode.NotFound, ErrorCodes.ASSET_NOT_FOUND);

        return entity;
    }
    public Asset GetAssetByTypeIdAndAssetNumber(int articleId, Domain.Enums.AssetType assetTypeId, int assetNumber)
    {
        var entity = Query().SingleOrDefault(e => e.ArticleId == articleId && e.TypeCode == assetTypeId && e.AssetNumber == assetNumber);
            return entity;
    }

    public Asset GetByType(Domain.Enums.AssetType assetType, int articleId)
        => Query().SingleOrDefault(e => e.TypeCode == assetType && e.ArticleId == articleId);


    public Asset? GetByIdOrDefault(int id) => Query().SingleOrDefault(e => e.Id == id);

    public async Task<Asset> GetAssetByTypeId(int articleId, Domain.Enums.AssetType typeId)
    {
        return await Where(x => x.ArticleId == articleId && x.TypeCode == typeId)
            .Include(x => x.Files)
            .Include(x => x.Article)
            .Include(x => x.Article)
            .ThenInclude(x => x.Journal)
            .Include(x => x.LatestFile)
            .FirstOrDefaultAsync();
    }

    public async Task<Asset> GetLatestFileByAssetTypeId(int articleId, Domain.Enums.AssetType typeId)
    {
        return await Where(x => x.ArticleId == articleId && x.TypeCode == typeId)
            .Include(x => x.Files)
            .Include(x => x.Article)
            .Include(x => x.LatestFile)
            .FirstOrDefaultAsync();
    }

    public async Task<Asset> GetWithArticleAsync(int assetId)
    {
        return await base.Entity
            .Include(x => x.LatestFile)
            .Include(x => x.Article)
            .FirstOrDefaultAsync(x => x.Id == assetId);
    }

    public async Task<Domain.Enums.AssetType> GetAssetTypeIdAsync(int articleId, int assetId)
    {
        var asset = await base.Entity.FirstOrDefaultAsync(x => x.ArticleId == articleId && x.Id == assetId);
        return asset.TypeCode;
    }
    public async Task<Asset> GetWithArticleAndFileAction(int assetId)
    {
        return await base.Entity
            .Include(x => x.Article)
            .Include(x => x.LatestFile)
                .ThenInclude(x=> x.File)
                    .ThenInclude(x => x.FileActions)
            .FirstOrDefaultAsync(x => x.Id == assetId);
    }

    public async Task<Asset> GetWithLatestFileAsync(int assetId)
    {
        return await base.Entity
        .Include(x => x.LatestFile)
        .FirstOrDefaultAsync(x => x.Id == assetId);
    }

    public async Task UpsertAsset(int articleId, int userId,
        string fileName, string extension, Domain.Enums.AssetType assetTypeId, string assetName,
        string fileServerId,
        int size,
        int version
    )
    {
        var asset = await CreateOrUpdateAsset(articleId, assetTypeId, userId, assetName);
        var file = CreateFile(fileName, extension, assetName, fileServerId, userId, assetTypeId, size, version);
        asset.Files.Add(file);
        await _context.SaveChangesAsync();
        //todo fix the latest file, nexxt line
        //asset.LatestFileId = file.Id;
        await _context.SaveChangesAsync();
    }
    private async Task<Asset> CreateOrUpdateAsset(int articleId,
        Domain.Enums.AssetType assetTypeId,
        int userId,
        string assetName)
    {
        var asset = await GetAssetByTypeId(articleId, assetTypeId);
        if (asset == null)
        {
            asset = new Asset()
            {
                ArticleId = articleId,
                Status = AssetStatus.Uploaded,
                TypeCode = assetTypeId,
                LastModifiedById = userId,
                //ModifiedDate = DateTime.Now,
                //CategoryId = await GetDefaultAssetCategory(assetTypeId),
                Name = assetName
            };
            _context.Add(asset);
        }
        return asset;
    }
    private static Domain.Entities.File CreateFile(string fileName,
        string extension,
        string assetName,
        string fileServerId,
        int userId,
        Domain.Enums.AssetType assetTypeId,
        int size,
        int version
        )
    {
        var file = new Domain.Entities.File()
        {
            Extension = extension,
            Name = assetName,
            OriginalName = fileName,
            FileServerId = fileServerId,
            Size = size,
            Version = version,
            //StatusId = (assetTypeId == ArticleAssetType.FRONTIERS_MANUSCRIPT || assetTypeId == ArticleAssetType.CROSSREF_XML) ?
            //ArticleFileStatusCode.SYSTEM_GENERATED :
            //ArticleFileStatusCode.UPLOADED,
        };
        file.FileActions.Add(CreateFileAction(userId));
        return file;
    }
    private static FileAction CreateFileAction(int userId)
    {
        return new FileAction()
        {
            TypeId = FileActionType.UPLOAD,
            Comment = string.Empty,
        };
    }
    public async Task<Asset> GetFileByAssetTypeIdAssetNumberAsync(int articleId, Domain.Enums.AssetType typeId, int assetNumber)
    {
        return await Where(x => x.ArticleId == articleId && x.TypeCode == typeId && x.AssetNumber == assetNumber)
            .Include(x => x.Files)
            .FirstOrDefaultAsync();
        
    }
    public async Task<Asset> GetFileByFileIdAsync(int articleId, int fileId)
    {
        return await Where(x => x.ArticleId == articleId)
            .Include(x => x.Files.Where(x=>x.Id == fileId)).FirstOrDefaultAsync(x=>x.Files.Any(y=>y.Id == fileId));
    }
    public async Task<Asset> GetFileByFileServerIdAsync(int articleId, string fileServerId)
    {
        return await Where(x => x.ArticleId == articleId)
            .Include(x => x.Files).FirstOrDefaultAsync(x => x.Files.Any(y => y.FileServerId == fileServerId));
    }

    #region PRIVATE
    private Dictionary<Domain.Enums.AssetType, AssetCategory> ConvertToDictionary(IList<Domain.Entities.AssetType> assetTypes)
    {
        if (assetTypes.IsNullOrEmpty())
        {
            return null;
        }
        return assetTypes.ToDictionary(x => x.Id, x => x.DefaultCategoryId);
    }

    public async Task<List<Asset>> GetAllAssetDetails(int articleId)
    {
       return await base.Entity.Where(a => a.ArticleId == articleId)
            .Include(a => a.Files)
            .ThenInclude(a =>a.FileActions)
            .Include(a => a.Files)
            .ToListAsync();
    }
    #endregion
}
