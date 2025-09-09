using ArticleHub.Domain.Entities;
using ArticleHub.Persistence.Repositories;
using Blocks.EntityFrameworkCore;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace ArticleHub.API.Articles.GetArticle;

public class GetArticleEndpoint : ICarterModule
{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
				app.MapGet("/articles/{articleId:int}", async (int articleId, [FromServices] Repository<Article> repository, CancellationToken ct) =>
				{
						var article = await repository.GetByIdOrThrowAsync(articleId, ct);

						return Results.Ok(article);
				})
				.RequireAuthorization() // allows all authenticated users
				.WithName("GetArticle")
				.WithTags("Articles")
				.Produces<Article>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.ProducesProblem(StatusCodes.Status401Unauthorized);
		}
}
