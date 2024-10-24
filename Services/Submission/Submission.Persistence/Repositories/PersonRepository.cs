using Microsoft.EntityFrameworkCore;
using Submission.Domain.Entities;

namespace Submission.Persistence.Repositories;

public class PersonRepository(ProductionDbContext dbContext) 
		: Repository<Person>(dbContext)
{
		public async Task<Person> GetByUserId(int userId, bool throwIfNotFound = true)
		{
				var person = await Query()
						.SingleAsync(e => e.UserId == userId);

				return ReturnOrThrow(person, throwIfNotFound);
		}
}

