using Blocks.Domain;

namespace Articles.Abstractions;

public abstract record DomainEvent(IArticleAction Action) : IDomainEvent;
