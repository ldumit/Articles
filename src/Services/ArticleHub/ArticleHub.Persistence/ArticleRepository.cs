using ArticleHub.Domain;
using Blocks.EntityFrameworkCore;

namespace ArticleHub.Persistence
{
		public class ArticleRepository : Repository<ArticleHubDbContext, Article>
		{
				public ArticleRepository(ArticleHubDbContext dbContext) : base(dbContext)
				{ }
		}
}
