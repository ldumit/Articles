using MediatR;
using Production.Domain.Enums;

namespace Production.Domain.Events;

public abstract record DomainEvent(int ArticleId, ActionType Action, int UserId, string? Comment) : INotification
{
}
