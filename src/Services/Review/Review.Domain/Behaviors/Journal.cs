namespace Review.Domain.Entities;

public partial class Journal
{
		public void AddArticle(Article article)
				=> _articles.Add(article);
}
