namespace ArticleHub.Persistence;

public class ArticleRepository(ArticleHubDbContext dbContext) 
		: RepositoryBase<ArticleHubDbContext, Article>(dbContext);
