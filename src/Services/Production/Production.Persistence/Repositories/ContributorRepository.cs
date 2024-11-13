using Blocks.Security;
using Blocks.Core;
using Microsoft.EntityFrameworkCore;
using Production.Domain.Entities;

namespace Production.Persistence.Repositories;

public class ContributorRepository(ProductionDbContext _dbContext) 
		: Repository<Article>(_dbContext), IArticleRoleChecker
{
		public async Task<bool> CheckRolesForUser(int? articleId, int? userId, IEnumerable<UserRoleType> roles)
		{
				if (articleId is null)
						return true; // the endpoint is not article specific

				if (userId is null || roles.IsNullOrEmpty())
						return false;

				//talk about code commetns, why are they usefull?				
				if(roles.Contains(UserRoleType.POF)) 
						return true; // admin users have access to all articles

				//todo - cache the actors?!
				return await _dbContext.ArticleContributors
						.AnyAsync(e => e.ArticleId == articleId && e.Person.UserId == userId && roles.Contains(e.Role));
		}
}
