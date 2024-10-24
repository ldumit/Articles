using Articles.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Submission.Domain.Entities;
using AssetType = Submission.Domain.Entities.AssetType;

namespace Submission.Persistence.Repositories;

public class AssetRepository(ProductionDbContext _dbContext, IMemoryCache _cache) 
    : Repository<Asset>(_dbContext)
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
            .SingleOrDefaultAsync(e => e.ArticleId == articleId && e.Type == assetTypeId && e.Number == assetNumber);
				return ReturnOrThrow(entity, throwNotFound);
		}

		public async Task<Asset?> GetByIdAsync(int articleId, int assetId, bool throwNotFound = true)
		{
				var entity = await Query()
						.SingleOrDefaultAsync(e => e.ArticleId == articleId && e.Id == assetId);
				return ReturnOrThrow(entity, throwNotFound);
		}

		public IEnumerable<AssetType> GetAssetTypes()
		=> _cache.GetOrCreate(entry => _dbContext.AssetTypes.AsNoTracking().ToList());

		public AssetType GetAssetType(Domain.Enums.AssetType type)
				=> GetAssetTypes().Single(e => e.Id == type);
}
