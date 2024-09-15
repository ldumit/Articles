using MediatR;
using Production.Domain.Enums;

namespace Production.Domain.Events;

public abstract record DomainEvent<TActiontype>(int ArticleId, TActiontype Action, int UserId, string? Comment) : INotification
		where TActiontype: Enum;
