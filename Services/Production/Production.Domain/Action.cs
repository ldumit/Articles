using Production.Domain.Enums;

namespace Production.Domain;

public interface IArticleAction : Articles.Abstractions.IArticleAction
{
		ActionType ActionType { get; }
}
