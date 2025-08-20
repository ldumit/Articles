namespace Blocks.Core.Security;

public interface IClaimsProvider
{
		public string GetClaimValue(string claimName);
		public int GetUserId();
		public int? TryGetUserId();
		public string GetUserEmail();
		public string GetUserName();
		public string GetUserRole();
}
