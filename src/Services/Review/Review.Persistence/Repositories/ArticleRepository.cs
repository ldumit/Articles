namespace Review.Persistence.Repositories;

public class ArticleRepository(ReviewDbContext dbContext) 
		: Repository<Article>(dbContext)
{
		protected override IQueryable<Article> Query()
		{
				return base.Entity
						.Include(e => e.Contributors)
						.Include(e => e.Assets);
								//.ThenInclude(e => e.TypeRef);
		}

		public async Task<Article> GetFullArticleByIdOrThrow(int id)
		{
				var article = await Entity
						.Include(e => e.Journal)
						.Include(e => e.SubmittedBy)
						.Include(e => e.Contributors)
								.ThenInclude(e => e.Person)
						.Include(e => e.Assets)
						.SingleOrDefaultAsync(e => e.Id == id);

				return ReturnOrThrow(article, true);
		}
}

