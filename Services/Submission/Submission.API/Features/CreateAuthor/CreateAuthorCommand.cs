using Submission.Domain.Enums;

namespace Submission.API.Features.CreateAuthor;

public record CreateAuthorCommand(
		string Email,
		string FirstName, 
		string LastName, 
		string? Title,
		bool IsCorrespondingAuthor, 
		string Affiliation);