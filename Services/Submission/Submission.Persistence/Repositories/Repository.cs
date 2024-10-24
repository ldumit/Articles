using Articles.Entitities;
using Articles.EntityFrameworkCore;

namespace Submission.Persistence.Repositories
{
		public class Repository<TEntity>(ProductionDbContext dbContext) 
				: Repository<ProductionDbContext, TEntity>(dbContext)
						where TEntity : class, IEntity<int>
		{
		}
}
