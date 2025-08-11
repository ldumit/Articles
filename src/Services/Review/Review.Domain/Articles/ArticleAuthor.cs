namespace Review.Domain.Articles;

public class ArticleAuthor : ArticleActor
{
		public ArticleAuthor(){ /*for EF.Core*/ }

		public ArticleAuthor(HashSet<ContributionArea> contributionAreas)
				=> _contributionAreas = contributionAreas;

		internal HashSet<ContributionArea> _contributionAreas = new();
		public IReadOnlyCollection<ContributionArea> ContributionAreas => _contributionAreas;
}
