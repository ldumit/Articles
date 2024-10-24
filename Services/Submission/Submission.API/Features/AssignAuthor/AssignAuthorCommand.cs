using Submission.Domain.Enums;

namespace Submission.API.Features.AssignAuthor;

public record AssignAuthorCommand(int AuthorId, ContributionArea[] ContributionAreas);
