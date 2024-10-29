using Articles.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Submission.Application.Features.Shared;
using Submission.Application.Features.UploadFiles;

namespace Submission.API.Endpoints
{
		public static class UploadSupplimentaryMaterialFileEndpoint
		{
				public static void Map(this IEndpointRouteBuilder app)
				{
						app.MapPost("api/articles/{articleId:int}/assets/supplimentary-materials",
								async ([FromRoute] int articleId, [FromForm] UploadSupplementaryFileCommand command, ISender sender) =>
								{
										command.ArticleId = articleId;
										var response = await sender.Send(command);
										return Results.Created($"/api/articles/{command.ArticleId}/assets/{response.Id}", response);
								})
						.RequireRoleAuthorization(Role.CORAUT)
						.WithName("UploadSupplimentaryMaterials")
						.WithTags("Assets")
						.Produces<IdResponse>(StatusCodes.Status201Created)
						.ProducesProblem(StatusCodes.Status400BadRequest)
						.ProducesProblem(StatusCodes.Status401Unauthorized)
						.DisableAntiforgery();
				}
		}
}
