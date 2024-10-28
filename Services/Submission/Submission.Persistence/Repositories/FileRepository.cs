using Microsoft.EntityFrameworkCore;

using File = Submission.Domain.Entities.File;

namespace Submission.Persistence.Repositories;

public class FileRepository(SubmissionDbContext _dbContext) 
    : Repository<File>(_dbContext)
{
    protected override IQueryable<File> Query()
    {
				return base.Entity
						.Include(x => x.Asset);
    }

		public async Task<File?> GetByIdAsync(int articleId, int fileId, bool throwNotFound = true)
		{
				var entity = await Query()
						.SingleOrDefaultAsync(e => e.Id == fileId && e.Asset!.ArticleId == articleId);
				return ReturnOrThrow(entity, throwNotFound);
		}
}
