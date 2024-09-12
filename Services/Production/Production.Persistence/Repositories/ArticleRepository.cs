using Articles.EntityFrameworkCore;
using Articles.Exceptions;
using Articles.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Production.Domain.Entities;
using System.Net;


namespace Production.Persistence.Repositories;


public class ArticleRepository(ProductionDbContext _dbContext, IMemoryCache _cache) 
		: RepositoryBase<ProductionDbContext, Article>(_dbContext)
{
		public virtual IList<Stage> GetStages()
				=> _cache.GetOrCreate(entry => _dbContext.Stages.ToList());


		protected override IQueryable<Article> Query()
		{
				return base.Entity
						.Include(e => e.CurrentStage)
						//.Include(e => e.CurrentStage.Stage)
						.Include(e => e.Actors);
		}

		public async Task<Article> GetByIdWithAssetsAsync(int id)
		{
				return await Query()
						 .Include(e => e.Assets)
								 .ThenInclude(e => e.LatestFile)
						.SingleAsync(e => e.Id == id);
		}
}

