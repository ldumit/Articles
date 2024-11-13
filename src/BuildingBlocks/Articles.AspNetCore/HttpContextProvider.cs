using Blocks.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Blocks.AspNetCore;

public interface IClaimsProvider
{
		public string GetClaimValue(string claimName);
		public int GetUserId();
		public string GetUserEmail();
		public string GetUserName();
		public string GetUserRole();
}

public interface IRouteProvider
{
		public string GetRouteValue(string key);
		public int? GetArticleId();

}

//talk - Solid Princile interface segregation
public class HttpContextProvider(IHttpContextAccessor _httpContextAccessor)
		: IClaimsProvider, IRouteProvider
{
		public string GetClaimValue(string claimName)
				=> _httpContextAccessor.GetClaimValue(claimName);
		public int GetUserId() 
				=> _httpContextAccessor.GetClaimValue(ClaimTypes.NameIdentifier).ToInt().Value;
		public string GetUserName()
				=> _httpContextAccessor.GetClaimValue(ClaimTypes.Name);		
		public string GetUserEmail() 
				=> _httpContextAccessor.GetClaimValue(ClaimTypes.Email);
		public string GetUserRole()
				=> _httpContextAccessor.GetClaimValue(ClaimTypes.Role);

		public string GetRouteValue(string key)
				=> _httpContextAccessor.GetRouteValue(key);
		public int? GetArticleId() 
				=> _httpContextAccessor.GetRouteValue("articleId").ToInt();

}

public static class HttpContextExtensions
{
		public static string GetRouteValue(this IHttpContextAccessor httpContextAccessor, string key)
		{
				var routeData = httpContextAccessor.HttpContext?.GetRouteData();
				return routeData?.Values[key]?.ToString();
		}

		public static string GetClaimValue(this IHttpContextAccessor httpContextAccessor, string claimType)
		{
				var user = httpContextAccessor.HttpContext?.User;
				return user?.FindFirst(claimType)?.Value;
		}
}
