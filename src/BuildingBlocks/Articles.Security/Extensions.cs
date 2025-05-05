using Microsoft.AspNetCore.Builder;

namespace Articles.Security;

public static class Extensions
{
		public static TBuilder RequireRoleAuthorization<TBuilder>(this TBuilder builder, params string[] roles) 
				where TBuilder : IEndpointConventionBuilder
				=> builder.RequireAuthorization(policy => policy.RequireRole(roles));

		public static TBuilder RequireRoleAuthorization<TBuilder>(this TBuilder builder, params UserRoleType[] roles)
				where TBuilder : IEndpointConventionBuilder
				=> builder.RequireAuthorization(policy => policy.RequireRole(string.Join(",", roles)));
}
