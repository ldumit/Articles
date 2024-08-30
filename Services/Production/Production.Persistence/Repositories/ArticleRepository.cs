using Articles.EntityFrameworkCore;
using Articles.System;
using Microsoft.Extensions.Caching.Memory;
using Production.Domain.Entities;


namespace Production.Persistence.Repositories;


public class ArticleRepository(ProductionDbContext _dbContext, IMemoryCache _cache) : RepositoryBase<Article>(_dbContext)
{
		public virtual IList<Stage> GetStages()
				=> _cache.GetOrCreate(entry => _dbContext.Stages.ToList());
}

