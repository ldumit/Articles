namespace Articles.Abstractions;

public interface IArticleAction
{
		int ArticleId { get; }
		int UserId { get; set; }
		string Comment { get; }
}
