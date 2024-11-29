using Microsoft.Extensions.Caching.Memory;

namespace Submission.Persistence.Repositories;

//public class CachedRepository<TEntity, TId>(SubmissionDbContext dbContext, IMemoryCache cache) 
//		: CachedRepositoryBase<SubmissionDbContext, TEntity, TId>(dbContext, cache)
//		where TEntity : class, IEntity<TId>, ICacheable
//		where TId : struct
//{
//}

public class AssetTypeRepository(SubmissionDbContext dbContext, IMemoryCache cache)
		: CachedRepository<SubmissionDbContext, AssetTypeDefinition, AssetType>(dbContext, cache);

