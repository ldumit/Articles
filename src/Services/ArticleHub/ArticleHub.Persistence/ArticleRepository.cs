using ArticleHub.Domain.Entities;
using Blocks.EntityFrameworkCore;

namespace ArticleHub.Persistence
{
    public class ArticleRepository : RepositoryBase<ArticleHubDbContext, Article>
		{
				public ArticleRepository(ArticleHubDbContext dbContext) : base(dbContext)
				{ }
		}
}
