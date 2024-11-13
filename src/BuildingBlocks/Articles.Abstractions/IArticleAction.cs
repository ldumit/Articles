using Blocks.Domain;

namespace Articles.Abstractions;

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
