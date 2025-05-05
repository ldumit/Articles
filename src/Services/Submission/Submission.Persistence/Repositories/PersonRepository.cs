namespace Submission.Persistence.Repositories;

public class PersonRepository(SubmissionDbContext dbContext) 
		: Repository<Person>(dbContext)
{
		public async Task<Person> GetByUserId(int userId)
				=> await Query()
						.SingleAsync(e => e.UserId == userId);

		public async Task<Person> GetByEmail(string email)
				=> await Query()
						.SingleAsync(e => e.Email.Value.ToLower() == email.ToLower()); //not all databases are case-insensitive
}

