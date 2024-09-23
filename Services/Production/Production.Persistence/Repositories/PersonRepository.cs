using Microsoft.EntityFrameworkCore;
using Production.Domain.Entities;

namespace Production.Persistence.Repositories;

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

