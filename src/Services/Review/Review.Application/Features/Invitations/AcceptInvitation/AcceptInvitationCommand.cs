using Review.Application.Features.Articles._Shared;
using Review.Domain.Articles.Enums;

namespace Review.Application.Features.Invitations.AcceptInvitation;

public record AcceptInvitationCommand(string Token)
    : ArticleCommand<AcceptInvitationResponse>
{
    public override ArticleActionType ActionType => ArticleActionType.InviteReviewer;
}

public record AcceptInvitationResponse(int ArticleId, int InvitationId, int ReviewerId);

public class RespondToInvitationCommandValidator : AbstractValidator<AcceptInvitationCommand>
{
    public RespondToInvitationCommandValidator()
    {
    }
}
