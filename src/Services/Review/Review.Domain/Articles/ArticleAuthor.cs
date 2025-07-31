namespace Review.Domain.Articles;

public class ArticleAuthor : ArticleActor
{
		public HashSet<ContributionArea> ContributionAreas { get; init; } = null!;
}
