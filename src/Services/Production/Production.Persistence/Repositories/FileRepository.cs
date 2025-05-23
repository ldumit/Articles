﻿using Microsoft.EntityFrameworkCore;

using File = Production.Domain.Entities.File;

namespace Production.Persistence.Repositories;

public class FileRepository(ProductionDbContext _dbContext) 
    : Repository<File>(_dbContext)
{
    protected override IQueryable<File> Query()
    {
				return base.Entity
						.Include(x => x.Asset);
    }

		public async Task<File> GetByIdAsync(int articleId, int fileId, bool throwNotFound = true)
		{
				var entity = await Query()
						.SingleOrDefaultAsync(e => e.Id == fileId && e.Asset!.ArticleId == articleId);
				return ReturnOrThrow(entity, throwNotFound);
		}
}
