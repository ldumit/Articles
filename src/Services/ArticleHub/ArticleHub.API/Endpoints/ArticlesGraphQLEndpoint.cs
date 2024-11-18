using ArticleHub.Persistence;

namespace ArticleHub.API.Endpoints;


public static class ArticlesGraphQLEndpoint
{
		public static void Map(this IEndpointRouteBuilder app)
		{
				//todo - create a custom model binder which will map the route parameter to the command
				//public class GenericModelBinder<T> : IModelBinder where T : class, new()
				app.MapPost("/graphql", async (string graphQLFilter, ArticleGraphQLQuery graphQLQuery) =>
				{
						var response = await graphQLQuery.GetArticlesAsync(graphQLFilter);

						return Results.Json(response.Items);
				})
				.WithName("ApproveArticle")
				.WithTags("Articles")
				//.Produces<IdResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.ProducesProblem(StatusCodes.Status401Unauthorized);
		}
}
