using Articles.Abstractions.Enums;
using Blocks.AspNetCore;
using Blocks.Core;
using Microsoft.AspNetCore.Authorization;

namespace Articles.Security;

public class ArticleAccessAuthorizationHandler(HttpContextProvider _httpProvider, IArticleRoleVerifier _articleRoleChecker)
		: AuthorizationHandler<ArticleRoleRequirement>
{
		protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ArticleRoleRequirement requirement)
		{
				//if (requirement.AllowedRoles.Any(context.User.IsInRole) && await HasUserRoleForArticle(requirement.AllowedRoles))
				//{
				//		context.Succeed(requirement);
				//}

				if (await HasUserRoleForArticle(requirement.AllowedRoles))
						context.Succeed(requirement);
		}

		private async Task<bool> HasUserRoleForArticle(IEnumerable<string> roles)
		{
				return await _articleRoleChecker
						.UserHasRoleForArticle(_httpProvider.GetArticleId(), _httpProvider.GetUserId(), roles.Select(r => r.ToEnum<UserRoleType>()));
		}
}
