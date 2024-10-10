using Articles.Domain;

namespace Articles.Abstractions;

public abstract record DomainEvent(int ArticleId, string Action, int UserId, string? Comment) : IDomainEvent;
