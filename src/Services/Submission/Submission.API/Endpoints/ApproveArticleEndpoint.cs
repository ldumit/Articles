using Microsoft.AspNetCore.Mvc;
using MediatR;
using Articles.Security;
using Submission.Application.Features.ApproveArticle;

namespace Submission.API.Endpoints;

public static class ApproveArticleEndpoint
{
		public static void Map(this IEndpointRouteBuilder app)
		{
				//todo - create a custom model binder which will map the route parameter to the command
				//public class GenericModelBinder<T> : IModelBinder where T : class, new()
				app.MapPost("api/articles/{articleId:int}:approve", async ([FromRoute] int articleId, [FromBody] ApproveArticleCommand command, ISender sender) =>
				{
						command.ArticleId = articleId;
						var response = await sender.Send(command);
						return Results.Ok(response);
				})
				.RequireRoleAuthorization(Role.EOF)
				.WithName("ApproveArticle")
				.WithTags("Articles")
				.Produces<IdResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.ProducesProblem(StatusCodes.Status401Unauthorized);
		}
}
