using Microsoft.EntityFrameworkCore;
using Production.Domain.Entities;

namespace Production.Persistence.Repositories;

public class ArticleRepository(ProductionDbContext dbContext) 
		: Repository<Article>(dbContext)
{
		protected override IQueryable<Article> Query()
		{
				return base.Entity
						.Include(e => e.Actors);
		}

		public async Task<Article> GetArticleWithAssetsById(int id, bool throwIfNotFound = true)
		{
				var article = await Entity
						.Include(e => e.Assets)
								.ThenInclude(e => e.Files)
						.SingleAsync(e => e.Id == id);

				return ReturnOrThrow(article, throwIfNotFound);
		}

		public async Task<Article> GetArticleSummaryById(int id, bool throwIfNotFound = true)
		{
				var article = await Entity
						.Include(e => e.Journal)
						.Include(e => e.Actors)
								.ThenInclude(e => e.Person)
						.SingleAsync(e => e.Id == id);

				return ReturnOrThrow(article, throwIfNotFound);
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
						 .Include(e => e.Assets.Where(e => e.Type == assetType && e.Number.Value == assetNumber))
								 .ThenInclude(e => e.CurrentFileLink)
										.ThenInclude(e => e.File)
						.Include(e => e.Assets)
								.ThenInclude(e => e.TypeRef)
						.SingleAsync(e => e.Id == id);

				return ReturnOrThrow(article, throwIfNotFound);
		}
}

