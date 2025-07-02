using Blocks.Entitities;

namespace Review.Persistence.Repositories;

public class Repository<TEntity>(ReviewDbContext dbContext) 
		: Repository<ReviewDbContext, TEntity>(dbContext)
				where TEntity : class, IEntity<int>
{
}
