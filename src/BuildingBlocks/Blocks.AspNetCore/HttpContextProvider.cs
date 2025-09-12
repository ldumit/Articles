using Blocks.Core;
using Blocks.Core.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Blocks.AspNetCore;

public interface IRouteProvider
{
		public string GetRouteValue(string key);
		public int? GetArticleId();

}

//talk - Solid Princile interface segregation
public class HttpContextProvider(IHttpContextAccessor _httpContextAccessor)
		: IClaimsProvider, IRouteProvider
{
		public string GetClaimValue(string claimName) =>
				TryGetClaimValue(claimName) ?? throw new InvalidOperationException($"Missing claim: {claimName}");
		public string? TryGetClaimValue(string claimName) =>
				_httpContextAccessor.GetClaimValue(claimName);

		public int GetUserId() 
				=> TryGetUserId() ?? throw new UnauthorizedAccessException($"Missing claim: {ClaimTypes.NameIdentifier}.");
		public int? TryGetUserId()
				=> GetClaimValue(ClaimTypes.NameIdentifier)?.ToInt();
		public string GetUserName()
				=> GetClaimValue(ClaimTypes.Name);		
		public string GetUserEmail() 
				=> GetClaimValue(ClaimTypes.Email);
		public string GetUserRole()
				=> GetClaimValue(ClaimTypes.Role);

		public string GetRouteValue(string key)
				=> _httpContextAccessor.GetRouteValue(key);
		public int? GetArticleId() 
				=> _httpContextAccessor.GetRouteValue("articleId").ToInt();

}

public static class HttpContextExtensions
{
		public static string? GetRouteValue(this IHttpContextAccessor httpContextAccessor, string key)
		{
				var routeData = httpContextAccessor.HttpContext?.GetRouteData();
				return routeData?.Values[key]?.ToString();
		}

		public static string? GetClaimValue(this IHttpContextAccessor httpContextAccessor, string claimType)
		{
				var user = httpContextAccessor.HttpContext?.User;
				return user?.FindFirstValue(claimType);
		}
}
