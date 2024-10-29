using Articles.Security;
using MediatR;
using Submission.Application.Features.CreateArticle;
using Submission.Application.Features.Shared;

namespace Submission.API.Endpoints;

public static class CreateArticleEndpoint
{
		public static void Map(this IEndpointRouteBuilder app)
		{
				app.MapPost("api/articles", async (CreateArticleCommand command, ISender sender) =>
				{
						var response = await sender.Send(command);
						return Results.Created($"/api/articles/{response.Id}", response);
				})
				.RequireRoleAuthorization(Role.CORAUT)
				.WithName("CreateArticle")
				.WithTags("Articles")
				.Produces<IdResponse>(StatusCodes.Status201Created)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status401Unauthorized);
		}
}