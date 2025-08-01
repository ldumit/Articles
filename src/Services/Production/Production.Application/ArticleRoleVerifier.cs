using Microsoft.EntityFrameworkCore;
using Articles.Security;

namespace Production.Application;

public class ArticleRoleVerifier(ProductionDbContext _dbContext) : IArticleRoleVerifier
{
		public async Task<bool> UserHasRoleForArticle(int? articleId, int? userId, IEnumerable<UserRoleType> roles)
		{
				// the endpoint is not article specific
				if (articleId is null)
						return true; 

				if (userId is null || roles.IsNullOrEmpty())
						return false;

				// editorial admin users have access to all articles
				if (roles.Contains(UserRoleType.POF)) 
						return true;

				return await _dbContext.ArticleContributors
						.AnyAsync(e => e.ArticleId == articleId && e.Person.UserId == userId && roles.Contains(e.Role));
		}
}
