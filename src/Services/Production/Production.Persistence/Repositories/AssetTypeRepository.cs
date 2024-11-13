using Articles.Abstractions.Enums;
using Articles.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Production.Domain.Entities;

namespace Production.Persistence.Repositories;

public class AssetTypeRepository(ProductionDbContext dbContext, IMemoryCache cache)
		: CachedRepositoryBase<ProductionDbContext, AssetTypeDefinition, AssetType>(dbContext, cache);
