using Articles.Entitities;
using Articles.EntityFrameworkCore;

namespace Production.Persistence.Repositories
{
		public class Repository<TEntity>(ProductionDbContext dbContext) 
				: Repository<ProductionDbContext, TEntity>(dbContext)
						where TEntity : class, IEntity
		{
		}
}
