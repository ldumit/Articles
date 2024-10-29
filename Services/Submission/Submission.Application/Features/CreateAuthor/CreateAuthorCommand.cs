using Submission.Application.Features.Shared;
using Submission.Domain.Enums;

namespace Submission.Application.Features.CreateAuthor;

public record CreateAuthorCommand(
		string Email,
		string FirstName,
		string LastName,
		string? Title,
		string Affiliation) : ArticleCommand
{
		public override ArticleActionType ActionType =>  ArticleActionType.CreateAuthor;
}