using Blocks.AspNetCore;
using Blocks.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Articles.Security;

public class ArticleRoleAuthorizationHandler(HttpContextProvider _httpProvider, IArticleRoleChecker _articleRoleChecker) 
		: AuthorizationHandler<RolesAuthorizationRequirement>
{
		protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
		{
				if (requirement.AllowedRoles.Any(context.User.IsInRole) && await HasUserRoleForArticle(requirement.AllowedRoles))
				{
						context.Succeed(requirement);
				}
		}

		private async Task<bool> HasUserRoleForArticle(IEnumerable<string> roles)
		{
				return await _articleRoleChecker
						.CheckRolesForUser(_httpProvider.GetArticleId(), _httpProvider.GetUserId(), roles.Select(r => r.ToEnum<UserRoleType>()));
		}
}
