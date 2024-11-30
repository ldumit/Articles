using ArticleHub.Domain;
using ArticleHub.Persistence;
//using Carter;

namespace ArticleHub.API.Endpoints;

public static class ArticlesGraphQLEndpoint //: ICarterModule
{
		public static void Map(this IEndpointRouteBuilder app)
		{
				app.MapPost("/graphql", async (string graphQLFilter, ArticleGraphQLQuery graphQLQuery) =>
				{
						var response = await graphQLQuery.GetArticlesAsync(graphQLFilter);

						return Results.Json(response.Items);
				})
				.WithName("GetArticles")
				.WithTags("Articles")
				.Produces<List<Article>>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.ProducesProblem(StatusCodes.Status401Unauthorized);
		}
}
