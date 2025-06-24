namespace Review.Domain.Entities;

public class ArticleAuthor : ArticleActor
{
		public HashSet<ContributionArea> ContributionAreas { get; init; } = null!;
}
