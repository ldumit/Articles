namespace Submission.Domain.Entities;

public class ArticleAuthor : ArticleContributor
{
		public HashSet<ContributionArea> ContributionAreas { get; init; } = null!;
}
