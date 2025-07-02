namespace Review.Persistence.Repositories;

public class ReviewerRepository(ReviewDbContext dbContext) 
		: Repository<Reviewer>(dbContext)
{
		protected override IQueryable<Reviewer> Query()
		{
				return base.Entity
						.Include(e => e.Specializations);
		}

		public async Task<Reviewer?> GetByUserIdAsync(int userId)
				=> await Query()
						.SingleOrDefaultAsync(e => e.UserId == userId);
}
