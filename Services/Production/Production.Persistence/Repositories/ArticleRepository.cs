using Articles.Abstractions;
using Microsoft.EntityFrameworkCore;
using Production.Domain.Entities;

namespace Production.Persistence.Repositories;

public class ArticleRepository(ProductionDbContext dbContext) 
		: Repository<Article>(dbContext)
{
		protected override IQueryable<Article> Query()
		{
				return base.Entity
						.Include(e => e.CurrentStage)
						//.Include(e => e.CurrentStage.Stage)
						.Include(e => e.Actors);
		}

		public async Task<Article> GetByIdWithAssetsAsync(int id, bool throwIfNotFound = true)
		{
				var article = await Query()
						 .Include(e => e.Assets)
								 .ThenInclude(e => e.CurrentFileLink)
										.ThenInclude(e => e.File)
						.SingleAsync(e => e.Id == id);
				
				return ReturnOrThrow(article, throwIfNotFound);
		}

		public async Task<Article> GetByIdWithSingleAssetAsync(int id, int assetId, bool throwIfNotFound = true)
		{
				var article = await Query()
						 .Include(e => e.Assets.Where(e => e.Id == assetId))
								 .ThenInclude(e => e.CurrentFileLink)
										.ThenInclude(e => e.File)
						.SingleAsync(e => e.Id == id);

				return ReturnOrThrow(article, throwIfNotFound);
		}

		public async Task<Article> GetByIdWithSingleAssetAsync(int id, Domain.Enums.AssetType assetType, byte assetNumber, bool throwIfNotFound = true)
		{
				var article = await Query()
						 .Include(e => e.Assets.Where(e => e.TypeCode == assetType && e.AssetNumber == assetNumber))
								 .ThenInclude(e => e.CurrentFileLink)
										.ThenInclude(e => e.File)
						.SingleAsync(e => e.Id == id);

				return ReturnOrThrow(article, throwIfNotFound);
		}
}

