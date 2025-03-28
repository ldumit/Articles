using Blocks.Domain;

namespace Articles.Abstractions;

//todo make Action a "set"(not "init") property 
public abstract record DomainEvent(IArticleAction Action) : IDomainEvent;
