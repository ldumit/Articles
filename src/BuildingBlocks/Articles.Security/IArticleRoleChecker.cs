namespace Blocks.Security;

public interface IArticleRoleChecker
{
		Task<bool> CheckRolesForUser(int? articleId, int? userId, IEnumerable<UserRoleType> roles);
}
