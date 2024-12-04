using Carter;
using Review.Application.Features.AssignEditor;

namespace Review.API.Endpoints;

public class AssignEditorEndpoint : ICarterModule
{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
				app.MapPost("api/articles/{articleId:int}/editor", async (int articleId, AssignEditorCommand command, ISender sender) =>
				{
						command.ArticleId = articleId;
						var response = await sender.Send(command);
						return Results.Ok(response);
				})
				.RequireRoleAuthorization(Role.EOF)
				.WithName("AssignEditor")
				.WithTags("Articles")
				.Produces<IdResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.ProducesProblem(StatusCodes.Status401Unauthorized);
		}
}