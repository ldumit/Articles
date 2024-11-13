using Articles.Abstractions.Enums;
using Blocks.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Production.Domain.Entities;

namespace Production.Persistence.Repositories;

public class AssetTypeRepository(ProductionDbContext dbContext, IMemoryCache cache)
		: CachedRepository<ProductionDbContext, AssetTypeDefinition, AssetType>(dbContext, cache);
