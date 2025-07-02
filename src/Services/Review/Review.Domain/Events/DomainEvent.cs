namespace Review.Domain.Events;

public abstract record DomainEvent(IArticleAction Action) : DomainEvent<IArticleAction>(Action);
