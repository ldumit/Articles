using Submission.Domain.Enums;

namespace Submission.Domain.Entities;

public class AuthorActor : ArticleActor
{
		public HashSet<ContributionArea> ContributionAreas { get; init; } = null!;
}
