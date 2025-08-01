using Microsoft.EntityFrameworkCore;
using Articles.Security;

namespace Review.Application;

public class ArticleRoleVerifier(ReviewDbContext _dbContext) : IArticleRoleVerifier
{
		public async Task<bool> UserHasRoleForArticle(int? articleId, int? userId, IEnumerable<UserRoleType> roles)
		{
				//the endpoint is not article specific
				if (articleId is null)
						return true;

				if (userId is null || roles.IsNullOrEmpty())
						return false;

				//talk about code commetns, why are they usefull?
				//editorial admins have access to all articles
				if (roles.Contains(UserRoleType.EOF)) 
						return true;

				return await _dbContext.ArticleActors
						.AnyAsync(e => e.ArticleId == articleId && e.Person.UserId == userId && roles.Contains(e.Role));
		}
}
