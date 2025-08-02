using Review.Application.Features.Articles._Shared;
using Review.Domain.Articles.Enums;

namespace Review.Application.Features.Invitations.AcceptInvitation;

public record AcceptInvitationCommand(string Token)
    : ArticleCommand<AcceptInvitationResponse>
{
    public override ArticleActionType ActionType => ArticleActionType.AcceptInvitation;
}

public record AcceptInvitationResponse(int ArticleId, int InvitationId, int ReviewerId);

public class AcceptInvitationCommandValidator : ArticleCommandValidator<AcceptInvitationCommand>
{
		public AcceptInvitationCommandValidator()
		{
				RuleFor(x => x.Token).NotEmpty();
		}
}
