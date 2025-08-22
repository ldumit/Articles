using Articles.Abstractions.Enums;

namespace Articles.Security;

public interface IArticleRoleVerifier
{
		Task<bool> UserHasRoleForArticle(int? articleId, int? userId, IEnumerable<UserRoleType> roles);
}
