using Review.Application.Features.Articles._Shared;

namespace Review.Application.Features.Invitations.AcceptInvitation;

public record AcceptInvitationCommand(string Token, bool Accepted)
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
