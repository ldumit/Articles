namespace Production.Persistence.Repositories;

public class ArticleRepository(ProductionDbContext dbContext) 
		: Repository<Article>(dbContext)
{
		protected override IQueryable<Article> Query()
		{
				return base.Entity
						.Include(e => e.Contributors);
		}

		public async Task<Article> GetArticleAssetsById(int id, bool throwIfNotFound = true)
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
						.Include(e => e.Contributors)
								.ThenInclude(e => e.Person)
						.SingleAsync(e => e.Id == id);

				return ReturnOrThrow(article, throwIfNotFound);
		}

		public async Task<Article> GetByIdWithAssetsAsync(int id, bool throwIfNotFound = true)
		{
				var article = await Query()
						 .Include(e => e.Assets)
								 //.ThenInclude(e => e.TypeDefinition)
						 .Include(e => e.Assets)
								 .ThenInclude(e => e.CurrentFileLink)
										.ThenInclude(e => e.File)

						.SingleAsync(e => e.Id == id);
				
				return ReturnOrThrow(article, throwIfNotFound);
		}
}

