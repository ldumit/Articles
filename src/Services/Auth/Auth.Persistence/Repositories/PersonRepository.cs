namespace Auth.Persistence.Repositories;

public class PersonRepository(AuthDBContext dbContext) 
		: RepositoryBase<AuthDBContext, Person>(dbContext)
{
		protected override IQueryable<Person> Query()
				=> base.Query().Include(p => p.User);

		public async Task<Person?> GetByEmailAsync(string email, CancellationToken ct = default)
				=> await Query().SingleOrDefaultAsync(p => p.Email.NormalizedEmail == email.ToUpperInvariant(), ct);
}
