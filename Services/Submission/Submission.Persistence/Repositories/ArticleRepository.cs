using Microsoft.EntityFrameworkCore;
using Submission.Domain.Entities;

namespace Submission.Persistence.Repositories;

public class ArticleRepository(SubmissionDbContext dbContext) 
		: Repository<Article>(dbContext)
{
		protected override IQueryable<Article> Query()
		{
				return base.Entity
						.Include(e => e.Actors)
						.Include(e => e.Assets);
								//.ThenInclude(e => e.TypeRef);
		}

		public async Task<Article> GetFullArticleById(int id, bool throwIfNotFound = true)
		{
				var article = await Entity
						.Include(e => e.Journal)
						.Include(e => e.SubmittedBy)
						.Include(e => e.Actors)
								.ThenInclude(e => e.Person)
						.Include(e => e.Assets)
						.SingleOrDefaultAsync(e => e.Id == id);

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
}

