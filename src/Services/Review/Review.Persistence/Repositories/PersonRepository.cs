﻿namespace Review.Persistence.Repositories;

public class PersonRepository(ReviewDbContext dbContext) 
		: Repository<Person>(dbContext)
{
		public async Task<Person> GetByUserId(int userId, bool throwIfNotFound = true)
		{
				var person = await Query()
						.SingleAsync(e => e.UserId == userId);

				return ReturnOrThrow(person, throwIfNotFound);
		}

		public async Task<Person> GetByEmail(string email, bool throwIfNotFound = true)
		{
				var person = await Query()
						.SingleOrDefaultAsync(e => e.Email.Value.ToLower() == email.ToLower()); //not all databases are case-insensitive

				return ReturnOrThrow(person, throwIfNotFound);
		}
}

