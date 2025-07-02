namespace Review.Domain.Events;

public record ReviewerInvited(ReviewInvitation Invitation, IArticleAction Action)
		: DomainEvent(Action);
