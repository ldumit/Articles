namespace Articles.Abstractions;

public interface IAction
{
		int CreatedById { get; set; }
		DateTime CreatedOn { get; }
		public string Action { get; }
}
public interface IArticleAction : IAction
{
		int ArticleId { get; }
		string? Comment { get; }
}

//decide - do we need to keep IArticleAction in this shared library or should we move it into Production domain?
public interface IArticleAction<TActionType> : IArticleAction
		where TActionType : Enum
{
		TActionType ActionType { get; }
}
