using Review.Application.Features.Invitations.AcceptInvitation;

namespace Review.API.Endpoints.Invitations;

public class AcceptInvitationEndpoint : ICarterModule
{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
				app.MapPost("api/articles/{articleId:int}/invitations/{token}:accept", async (int articleId, AcceptInvitationCommand command, ISender sender) =>
				{
						command.ArticleId = articleId;
						var response = await sender.Send(command);
						return Results.Ok(response);
				})
				//.RequireRoleAuthorization(Role.REV)
				.AllowAnonymous()
				.WithName("Accept Invitation")
				.WithTags("Invitations")
				.Produces<IdResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.ProducesProblem(StatusCodes.Status401Unauthorized);
		}
}