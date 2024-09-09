namespace Articles.Abstractions;


public interface IArticleAction
{
		int ArticleId { get; set; }
		int UserId { get; set; }
}
public interface IArticleAction<TActionType> : IArticleAction
		where TActionType : Enum
{
		TActionType ActionType { get; }
		string ActionComment { get; }
}
