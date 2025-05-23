﻿using Microsoft.AspNetCore.Mvc;
using Submission.Application.Features.UploadFiles;

namespace Submission.API.Endpoints;

public static class UploadSupplimentaryMaterialFileEndpoint
{
		public static void Map(this IEndpointRouteBuilder app)
		{
				app.MapPost("api/articles/{articleId:int}/assets/supplimentary-materials:upload",
				async ([FromRoute] int articleId, [FromForm] UploadSupplementaryFileCommand command, ISender sender) =>
				{
						var response = await sender.Send(command with { ArticleId = articleId });
						return Results.Created($"/api/articles/{command.ArticleId}/assets/{response.Id}:download", response);
				})
				.RequireRoleAuthorization(Role.CORAUT) //todo - is ArticleRoleAuthorizationHandler enough? test it! 
				.WithName("UploadSupplimentaryMaterials")
				.WithTags("Assets")
				.Produces<IdResponse>(StatusCodes.Status201Created)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.ProducesProblem(StatusCodes.Status401Unauthorized)
				.DisableAntiforgery();
		}
}
