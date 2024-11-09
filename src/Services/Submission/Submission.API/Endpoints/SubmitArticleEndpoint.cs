using Articles.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Submission.Application.Features.SubmitArticle;

namespace Submission.API.Endpoints;

public static class SubmitArticleEndpoint
{
		public static void Map(this IEndpointRouteBuilder app)
		{
				app.MapPost("api/articles/{articleId:int}:submit", async ([FromRoute] int articleId, [FromBody] SubmitArticleCommand command, ISender sender) =>
				{
						command.ArticleId = articleId; 
						var response = await sender.Send(command);
						return Results.Ok(response);
				})				
				.RequireRoleAuthorization(Role.CORAUT)
				.WithName("SubmitArticle")
				.WithTags("Articles")
				.Produces<IdResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status401Unauthorized);
		}
}
