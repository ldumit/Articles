using Review.Application.Features.Articles.RejectArticle;

namespace Review.API.Endpoints;

public class RejectArticleEndpoint : ICarterModule
{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
				app.MapPost("api/articles/{articleId:int}:reject", async (int articleId, RejectArticleCommand command, ISender sender) =>
				{
						command.ArticleId = articleId;
						var response = await sender.Send(command);
						return Results.Ok(response);
				})
				.RequireRoleAuthorization(Role.EOF)
				.WithName("RejectArticle")
				.WithTags("Articles")
				.Produces<IdResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status401Unauthorized);
		}
}
