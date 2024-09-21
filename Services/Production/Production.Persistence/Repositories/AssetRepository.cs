using Articles.Entitities;
using Articles.EntityFrameworkCore;
using Articles.Exceptions;
using Articles.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Production.Domain.Entities;
using Production.Domain.Enums;

using AssetType = Production.Domain.Entities.AssetType;

namespace Production.Persistence.Repositories;

public class AssetRepository(ProductionDbContext _dbContext, IMemoryCache _cache) 
    : RepositoryBase<ProductionDbContext, Asset>(_dbContext)
{
    protected override IQueryable<Asset> Query()
    {
        return base.Entity
						.Include(x => x.Article)
						.Include(e => e.CurrentFileLink)
                .ThenInclude(e => e.File);
    }

    public async Task<Asset?> GetByTypeAndNumberAsync(int articleId, Domain.Enums.AssetType assetTypeId, int assetNumber = 0, bool throwNotFound = true)
    {
        var entity = await Query()
            .SingleOrDefaultAsync(e => e.ArticleId == articleId && e.TypeCode == assetTypeId && e.AssetNumber == assetNumber);
				return ReturnOrThrow(entity, throwNotFound);
		}

		public async Task<Asset?> GetByIdAsync(int articleId, int assetId, bool throwNotFound = true)
		{
				var entity = await Query()
						.SingleOrDefaultAsync(e => e.ArticleId == articleId && e.Id == assetId);
				return ReturnOrThrow(entity, throwNotFound);
		}

		public IEnumerable<AssetType> GetAssetTypes()
		=> _cache.GetOrCreate(entry => _dbContext.AssetTypes.ToList());

		public AssetType GetAssetType(Domain.Enums.AssetType type)
				=> GetAssetTypes().Single(e => e.Id == type);
}
