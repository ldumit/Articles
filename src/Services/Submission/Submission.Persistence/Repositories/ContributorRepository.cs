using Articles.Security;
using Articles.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Submission.Domain.Entities;

namespace Submission.Persistence.Repositories;

public class ContributorRepository(SubmissionDbContext _dbContext, IMemoryCache _cache) 
		: Repository<Article>(_dbContext), IArticleRoleChecker
{
		public async Task<bool> CheckRolesForUser(int? articleId, int? userId, IEnumerable<UserRoleType> roles)
		{
				if(articleId is null)
						return true; // the endpoint is not article specific

				if (userId is null || roles.IsNullOrEmpty())
						return false;

				//talk about code commetns, why are they usefull?
				// admin users have access to all articles
				if(roles.Contains(UserRoleType.EOF)) 
						return true;

				return await _dbContext.ArticleContributors
						.AnyAsync(e => e.ArticleId == articleId && e.Person.UserId == userId && roles.Contains(e.Role));
		}
}
