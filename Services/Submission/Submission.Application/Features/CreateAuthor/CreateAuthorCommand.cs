using Submission.Domain.Enums;

namespace Submission.Application.Features.CreateAuthor;

public record CreateAuthorCommand(
		string Email,
		string FirstName, 
		string LastName, 
		string? Title,
		bool IsCorrespondingAuthor, 
		string Affiliation);