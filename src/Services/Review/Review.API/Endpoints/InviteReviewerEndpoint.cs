using Review.Application.Features.Invitations.InviteReviewer;

namespace Review.API.Endpoints;

public class InviteReviewerEndpoint: ICarterModule
{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
				app.MapPost("api/articles/{articleId:int}/reviewers", async (int articleId, InviteReviewerCommand command, ISender sender) =>
				{
						command.ArticleId = articleId;
						var response = await sender.Send(command);
						return Results.Ok(response);
				})
				.RequireRoleAuthorization(Role.REVED)
				.WithName("InviteReviewer")
				.WithTags("Articles")
				.Produces<IdResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.ProducesProblem(StatusCodes.Status401Unauthorized);
		}
}