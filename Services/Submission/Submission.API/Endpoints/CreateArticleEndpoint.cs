using Articles.Security;
using Submission.Application.Features.CreateArticle;

namespace Submission.API.Endpoints;

public static class CreateArticleEndpoint
{
		public static void MapCreateArticleEndpoint(this IEndpointRouteBuilder app)
		{
				app.MapPost("api/articles", async (CreateArticleCommand command, ISender sender) =>
				{
						var response = await sender.Send(command);
						return Results.Created($"/api/articles/{response.Id}", response);
				})
				.RequireAuthorization(Role.CORAUT)
				.WithName("CreateArticle")
				.WithTags("Articles")
				.Produces<CreateArticleResponse>(StatusCodes.Status201Created)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status401Unauthorized);
		}
}