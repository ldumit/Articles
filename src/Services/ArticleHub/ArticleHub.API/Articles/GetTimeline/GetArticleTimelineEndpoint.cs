using Carter;
using Blocks.EntityFrameworkCore;
using ArticleHub.Domain.Entities;
using ArticleHub.Persistence.Repositories;

namespace ArticleHub.API.Articles.GetArticleTimeline;

public class GetArticleTimelineEndpoint : ICarterModule
{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
				app.MapGet("/articles/{articleId:int}/timeline", async (int articleId, Repository<Article> repository, CancellationToken ct) =>
				{
						//todo - replace it with the actual timeline
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
