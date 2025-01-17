using Microsoft.AspNetCore.Mvc;
using Review.Application.Features.Articles.UploadFiles.UploadReviewReport;

namespace Review.API.Endpoints
{
    public class UploadReviewReportEndpoint : ICarterModule
		{
				public void AddRoutes(IEndpointRouteBuilder app)
				{
						app.MapPost("api/articles/{articleId:int}/assets/manuscript:upload",
								async ([FromRoute] int articleId, [FromForm] UploadReviewReportCommand command, ISender sender) =>
						{
								command.ArticleId = articleId;
								var response = await sender.Send(command);
								return Results.Created($"/api/articles/{command.ArticleId}/assets/{response.Id}:download", response);
						})
						.RequireRoleAuthorization(Role.CORAUT)
						.WithName("UploadReviewReport")
						.WithTags("Assets")
						.Produces<IdResponse>(StatusCodes.Status201Created)
						.ProducesProblem(StatusCodes.Status400BadRequest)
						.ProducesProblem(StatusCodes.Status404NotFound)
						.ProducesProblem(StatusCodes.Status401Unauthorized)
						.DisableAntiforgery(); // because of IFormFile
				}
		}
}
