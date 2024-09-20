namespace Articles.Abstractions;

public interface IArticleCommand
{
		int ArticleId { get; }
		int UserId { get; set; }
		string Comment { get; }
}

//decide - do we need to keep IArticleAction in this shared library or should we move it into Production domain?
public interface IArticleAction<TActionType> : IArticleCommand
		where TActionType : Enum
{
		TActionType ActionType { get; }
}
