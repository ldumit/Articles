using Articles.Security;
using Microsoft.EntityFrameworkCore;

namespace Submission.Application;

public class ArticleRoleVerifier(SubmissionDbContext _dbContext) : IArticleRoleVerifier
{
		public async Task<bool> UserHasRoleForArticle(int? articleId, int? userId, IEnumerable<UserRoleType> roles)
		{
				//the endpoint is not article specific
				if (articleId is null) 
						return true; 

				if (userId is null || roles.IsNullOrEmpty()) 
						return false;

				//talk about code comments, why are they usefull?
				//editorial admin users have access to all articles
				if(roles.Contains(UserRoleType.EOF)) 
						return true;

				return await _dbContext.ArticleActors
						.AnyAsync(e => e.ArticleId == articleId && e.Person.UserId == userId && roles.Contains(e.Role));
		}
}
