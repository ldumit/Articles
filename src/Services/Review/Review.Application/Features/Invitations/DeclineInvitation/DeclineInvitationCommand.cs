using Review.Application.Features.Articles.Shared;
using Review.Domain.Shared.Enums;

namespace Review.Application.Features.Invitations.AcceptInvitation;

public record DeclineInvitationCommand(string Token)
    : ArticleCommand<DeclineInvitationResponse>
{
    public override ArticleActionType ActionType => ArticleActionType.DeclineInvitation;
}

public record DeclineInvitationResponse(int ArticleId, int InvitationId);

public class DeclineInvitationCommandValidator : ArticleCommandValidator<DeclineInvitationCommand>
{
    public DeclineInvitationCommandValidator()
    {
				RuleFor(x => x.Token).NotEmpty();
		}
}
