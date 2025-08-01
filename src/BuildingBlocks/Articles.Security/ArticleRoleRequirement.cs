using Microsoft.AspNetCore.Authorization;

namespace Articles.Security;

public class ArticleRoleRequirement : IAuthorizationRequirement
{
		public IEnumerable<string> AllowedRoles { get; }

		public ArticleRoleRequirement(IEnumerable<string> allowedRoles)
		{
				AllowedRoles = allowedRoles;
		}
}
