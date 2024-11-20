using Articles.Abstractions.Enums;
using Blocks.Core;
using Blocks.Core.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Submission.Domain.Entities;
using AssetTypeDefinition = Submission.Domain.Entities.AssetTypeDefinition;

namespace Submission.Persistence.Repositories;

public class AssetRepository(SubmissionDbContext _dbContext, IMemoryCache _cache) 
    : Repository<Asset>(_dbContext)
{
    protected override IQueryable<Asset> Query()
    {
				return base.Entity
						.Include(x => x.Article);
						//.Include(x => x.TypeRef);
    }

		public async Task<Asset?> GetByTypeAndNumberAsync(int articleId, AssetType assetTypeId, int assetNumber = 0, bool throwNotFound = true)
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

		public IEnumerable<AssetTypeDefinition> GetAssetTypes()
				=> _cache.GetOrCreate(entry => _dbContext.AssetTypes.AsNoTracking().ToList());

		public AssetTypeDefinition GetAssetType(AssetType type)
				=> GetAssetTypes().Single(e => e.Id == type);
}
