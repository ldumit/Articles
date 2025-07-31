using Review.Application.Features.Articles._Shared;
using Review.Domain.Articles.Enums;

namespace Review.Application.Features.Invitations.AcceptInvitation;

public record AcceptInvitationCommand(string Token)
    : ArticleCommand<IdResponse>
{
    public override ArticleActionType ActionType => ArticleActionType.InviteReviewer;
}

public class RespondToInvitationCommandValidator : AbstractValidator<AcceptInvitationCommand>
{
    public RespondToInvitationCommandValidator()
    {
    }
}
