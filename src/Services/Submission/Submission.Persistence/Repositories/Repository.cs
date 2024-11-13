using Blocks.Entitities;
using Blocks.EntityFrameworkCore;

namespace Submission.Persistence.Repositories
{
		public class Repository<TEntity>(SubmissionDbContext dbContext) 
				: Repository<SubmissionDbContext, TEntity>(dbContext)
						where TEntity : class, IEntity<int>
		{
		}
}
