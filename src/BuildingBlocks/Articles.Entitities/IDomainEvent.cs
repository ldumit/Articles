using FastEndpoints;
using MediatR;

namespace Articles.Domain;

public interface IDomainEvent : INotification, IEvent;
