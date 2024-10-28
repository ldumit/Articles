using Submission.Domain.Enums;

namespace Submission.Application.Features.AssignAuthor;

public record AssignAuthorCommand(int AuthorId, ContributionArea[] ContributionAreas);
