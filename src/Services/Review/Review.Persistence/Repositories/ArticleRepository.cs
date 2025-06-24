namespace Review.Persistence.Repositories;

public class ArticleRepository(ReviewDbContext dbContext) 
		: Repository<Article>(dbContext)
{
		protected override IQueryable<Article> Query()
		{
				return base.Entity
						.Include(e => e.Actors)
						.Include(e => e.Assets);
								//.ThenInclude(e => e.TypeRef);
		}

		public async Task<Article> GetFullArticleByIdOrThrow(int id)
		{
				var article = await Entity
						.Include(e => e.Journal)
						.Include(e => e.SubmittedBy)
						.Include(e => e.Actors)
								.ThenInclude(e => e.Person)
						.Include(e => e.Assets)
						.SingleOrDefaultAsync(e => e.Id == id);

				return ReturnOrThrow(article, true);
		}
		public async Task<Article> GetArticleByIdWithInvitationsOrThrow(int id)
		{
				var article = await Entity
						.Include(e => e.Journal)
						.Include(e => e.Invitations)
						.SingleOrDefaultAsync(e => e.Id == id);

				return ReturnOrThrow(article, true);
		}
}

