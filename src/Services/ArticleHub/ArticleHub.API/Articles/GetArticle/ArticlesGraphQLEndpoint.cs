using ArticleHub.Domain.Entities;
using ArticleHub.Persistence;
using Carter;

namespace ArticleHub.API.Articles.SearchArticles;

public class GetArticleEndpoint : ICarterModule
{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
				app.MapPost("/articles/{articleId:int}", async (GraphQLFilterQuery graphQLFilter, ArticleGraphQLQuery graphQLQuery) =>
				{
						var response = await graphQLQuery.GetArticlesAsync(graphQLFilter.Filter);

						return Results.Json(response.Items);
				})
				.RequireAuthorization() // allows all authenticated users
				.WithName("GetArticles")
				.WithTags("Articles")
				.Produces<List<Article>>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.ProducesProblem(StatusCodes.Status401Unauthorized);
		}
}
