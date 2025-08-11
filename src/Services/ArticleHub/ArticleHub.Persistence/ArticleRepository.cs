using ArticleHub.Domain.Entities;

namespace ArticleHub.Persistence
{
    public class ArticleRepository : RepositoryBase<ArticleHubDbContext, Article>
		{
				public ArticleRepository(ArticleHubDbContext dbContext) : base(dbContext)
				{ }
		}
}
