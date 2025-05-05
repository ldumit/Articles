namespace Submission.Persistence.Repositories;

public class ArticleRepository(SubmissionDbContext dbContext) 
		: Repository<Article>(dbContext)
{
		protected override IQueryable<Article> Query()
		{
				return base.Entity
						.Include(e => e.Contributors)
						.Include(e => e.Assets);
		}

		public async Task<Article?> GetFullArticleById(int id)
		{
				return await Entity
						.Include(e => e.Journal)
						.Include(e => e.SubmittedBy)
						.Include(e => e.Contributors)
								.ThenInclude(e => e.Person)
						.Include(e => e.Assets)
						.SingleOrDefaultAsync(e => e.Id == id);
		}
}

