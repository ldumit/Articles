using Review.Domain.Invitations.Enums;

namespace Review.Application.Dtos;

public record ReviewInvitationDto(
		int Id,
		int ArticleId,
		string Email,
		string FirstName,
		string LastName,
		DateTime SentOn,
		DateTime ExpiresOn,
		InvitationStatus Status,
		string Token
		);
