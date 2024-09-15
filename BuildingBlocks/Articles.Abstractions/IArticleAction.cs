namespace Articles.Abstractions;

public interface IArticleAction
{
		int ArticleId { get; }
		int UserId { get; set; }
		string Comment { get; }
}

public interface IArticleAction<TActionType> : IArticleAction
		where TActionType : Enum
{
		//int ArticleId { get; }
		//int UserId { get; set; }
		//string Comment { get; }
		TActionType ActionType { get; }
}
