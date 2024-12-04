using Review.Application.Features.AcceptArticle;

namespace Review.API.Endpoints;

public class AcceptArticleEndpoint : ICarterModule
{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
				app.MapPost("api/articles/{articleId:int}:accept", async (int articleId, AcceptArticleCommand command, ISender sender) =>
				{
						command.ArticleId = articleId;
						var response = await sender.Send(command);
						return Results.Ok(response);
				})
				.RequireRoleAuthorization(Role.EOF)
				.WithName("AcceptArticle")
				.WithTags("Articles")
				.Produces<IdResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.ProducesProblem(StatusCodes.Status401Unauthorized);
		}
}
