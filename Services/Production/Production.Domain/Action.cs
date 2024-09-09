using Production.Domain.Enums;

namespace Production.Domain;

public record Action(ActionType ActionType, int UserId, string? Comment);


public interface IArticleAction : Articles.Abstractions.IArticleAction<ActionType>;
