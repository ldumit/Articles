using Articles.Security;
using MediatR;
using Submission.Application.Features.AssignAuthor;
using Submission.Application.Features.Shared;

namespace Submission.API.Endpoints;

public static class AssignAuthorEndpoint
{
		public static void Map(this IEndpointRouteBuilder app)
		{
				app.MapPut("api/articles/{articleId:int}/authors", async (int articleId, AssignAuthorCommand command, ISender sender) =>
				{
						command.ArticleId = articleId;
						var response = await sender.Send(command);
						return Results.Ok(response);
				})
				.RequireRoleAuthorization(Role.CORAUT)
				.WithName("AssignAuthor")
				.WithTags("Articles")
				.Produces<IdResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.ProducesProblem(StatusCodes.Status401Unauthorized);
		}
}