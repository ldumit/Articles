namespace Submission.API.Endpoints;

public static class EndpointRegistration
{
		public static void MapAllEndpoints(this IEndpointRouteBuilder app)
		{
				app.MapCreateArticleEndpoint();
				// Add more endpoint mappings as needed
		}
}
