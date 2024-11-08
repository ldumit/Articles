using Articles.Abstractions;
using Submission.Domain.Enums;

namespace Submission.Domain.Events;

public record ArticleCreatedDomainEvent(IArticleAction action, int ArticleId, int JournalId, string Title, ArticleType Type, string ScopeStatement)
		: DomainEvent(action)
{
    //public IArticleAction Action2 { get; init; }
}

