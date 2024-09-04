using Articles.EntityFrameworkCore;
using Articles.Exceptions;
using Articles.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Production.Domain.Entities;
using System.Net;


namespace Production.Persistence.Repositories;


public class ArticleRepository(ProductionDbContext _dbContext, IMemoryCache _cache) : RepositoryBase<ProductionDbContext, Article>(_dbContext)
{
		public virtual IList<Stage> GetStages()
				=> _cache.GetOrCreate(entry => _dbContext.Stages.ToList());


		protected override IQueryable<Article> Query()
		{
				return base.Entity
						.Include(e => e.CurrentStage)
						.Include(e => e.Actors);
		}
		public override Article GetById(int id, bool throwNotFound = true)
		{
				var entity = Query().SingleOrDefault(e => e.Id == id);
				if (throwNotFound && entity is null)
						throw new NotFoundException();

				return entity;
		}
}

