using Blocks.Entitities;
using Blocks.EntityFrameworkCore;

namespace Production.Persistence.Repositories;

public class Repository<TEntity>(ProductionDbContext dbContext) 
		: RepositoryBase<ProductionDbContext, TEntity>(dbContext)
				where TEntity : class, IEntity<int>
{
}
