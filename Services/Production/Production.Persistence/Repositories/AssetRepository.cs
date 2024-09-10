using Articles.EntityFrameworkCore;
using Articles.Exceptions;
using Articles.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Production.Domain.Entities;
using Production.Domain.Enums;

using AssetType = Production.Domain.Entities.AssetType;

namespace Production.Persistence.Repositories;

public class AssetRepository(ProductionDbContext _dbContext, IMemoryCache _cache) : RepositoryBase<ProductionDbContext, Asset>(_dbContext)
{
    protected override IQueryable<Asset> Query()
    {
        return base.Entity
						//.Include(x => x.Article)
						.Include(e => e.LatestFileRef)
                .ThenInclude(e => e.File);
    }

    public async Task<Asset?> GetByTypeAndNumber(int articleId, Domain.Enums.AssetType assetTypeId, int assetNumber = 0)
    {
        return await Query()
            .SingleOrDefaultAsync(e => e.ArticleId == articleId && e.TypeCode == assetTypeId && e.AssetNumber == assetNumber);
    }
}
