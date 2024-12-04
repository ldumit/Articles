using Microsoft.Extensions.Caching.Memory;

namespace Review.Persistence.Repositories;

//public class CachedRepository<TEntity, TId>(SubmissionDbContext dbContext, IMemoryCache cache) 
//		: CachedRepositoryBase<SubmissionDbContext, TEntity, TId>(dbContext, cache)
//		where TEntity : class, IEntity<TId>, ICacheable
//		where TId : struct
//{
//}

public class AssetTypeRepository(ReviewDbContext dbContext, IMemoryCache cache)
		: CachedRepository<ReviewDbContext, AssetTypeDefinition, AssetType>(dbContext, cache);

