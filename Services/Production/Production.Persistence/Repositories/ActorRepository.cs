using Articles.EntityFrameworkCore;
using Articles.Security;
using Articles.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Production.Domain.Entities;

namespace Production.Persistence.Repositories;

public class ActorRepository(ProductionDbContext _dbContext, IMemoryCache _cache) 
		: RepositoryBase<ProductionDbContext, Article>(_dbContext), IArticleRoleChecker
{
		public async Task<bool> CheckRolesForUser(int? articleId, int? userId, IEnumerable<UserRoleType> roles)
		{
				if (articleId is null || userId is null || roles.IsNullOrEmpty())
						return false;

				//talk about code commetns, why are they usefull?
				// admin users have access to all articles
				if(roles.Contains(UserRoleType.POF)) 
						return true;

				//todo - cache the actors?!
				return await _dbContext.ArticleActors
						.AnyAsync(e => e.ArticleId == articleId && e.Person.UserId == userId && roles.Contains(e.Role));
		}
}
