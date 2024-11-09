namespace Articles.Abstractions;

public interface IAction
{
		int CreatedById { get; set; }
		DateTime CreatedOn { get; }
		public string Action { get; }
		string? Comment { get; }
}

public interface IArticleAction : IAction
{
		int ArticleId { get; }
}

public interface IAction<TActionType> : IAction
		where TActionType : Enum
{
		TActionType ActionType { get; }
}


//decide - do we need to keep IArticleAction in this shared library or should we move it into Production domain?
public interface IArticleAction<TActionType> : IAction<TActionType>, IArticleAction
		where TActionType : Enum
{
}
