using Review.Domain.Reviewers;

namespace Review.Domain.Articles;

public class ArticleAuthor : ArticleActor
{
		internal HashSet<ContributionArea> _contributionAreas = new();
		public IReadOnlyCollection<ContributionArea> ContributionAreas => _contributionAreas;
}
