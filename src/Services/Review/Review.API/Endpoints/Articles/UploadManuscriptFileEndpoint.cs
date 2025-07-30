using Microsoft.AspNetCore.Mvc;
using Review.Application.Features.Articles.UploadFiles.UploadManuscriptFile;

namespace Review.API.Endpoints.Articles
{
    public class UploadManuscriptFileEndpoint : ICarterModule
		{
				public void AddRoutes(IEndpointRouteBuilder app)
				{
						app.MapPost("api/articles/{articleId:int}/assets/review-reports:upload",
								async ([FromRoute] int articleId, [FromForm] UploadManuscriptFileCommand command, ISender sender) =>
						{
								command.ArticleId = articleId;
								var response = await sender.Send(command);
								return Results.Created($"/api/articles/{command.ArticleId}/assets/{response.Id}:download", response);
						})
						.RequireRoleAuthorization(Role.CORAUT)
						.WithName("UploadManuscript")
						.WithTags("Assets")
						.Produces<IdResponse>(StatusCodes.Status201Created)
						.ProducesProblem(StatusCodes.Status400BadRequest)
						.ProducesProblem(StatusCodes.Status404NotFound)
						.ProducesProblem(StatusCodes.Status401Unauthorized)
						.DisableAntiforgery(); // because of IFormFile
				}
		}
}
